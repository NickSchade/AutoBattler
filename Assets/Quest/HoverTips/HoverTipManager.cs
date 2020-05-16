using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class HoverTipManager : MonoBehaviour, IQuestEventHandler
{
    public StatBlock _statBlockPrefab;
    public ActionBlock _actionBlockPrefab;
    public EffectTip _effectTipPrefab;

    public Canvas _statCanvas;
    Dictionary<Character, StatBlock> _statBlocks;

    public Canvas _actionCanvas;
    Dictionary<Ability, ActionBlock> _actionBlock;

    public Canvas _textCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        _statBlocks = new Dictionary<Character, StatBlock>();
        _actionBlock = new Dictionary<Ability, ActionBlock>();
    }
    private void Update()
    {
        foreach (Action ability in _actionBlock.Keys.ToList())
        {
            Vector3 p = ability._character._screenManager._cinematics._camera.WorldToScreenPoint(ability._character.transform.localPosition);
            _actionBlock[ability].transform.position = new Vector3(p.x, p.y + 75, p.z);

            if (!ability._character._isAlive)
            {
                Destroy(_actionBlock[ability].gameObject);
                _actionBlock.Remove(ability);
            }
        }
    }

    // Update is called once per frame
    public void UpdateCharacterStats(List<Character> characters)
    {
        foreach (Character character in characters)
        {
            if (!character._isAlive)
            {
                if (_statBlocks.ContainsKey(character))
                    Destroy(_statBlocks[character].gameObject);
                _statBlocks.Remove(character);
            }
            else
            {
                if (!_statBlocks.ContainsKey(character))
                    _statBlocks[character] = Instantiate(_statBlockPrefab, _statCanvas.transform);

                _statBlocks[character].UpdateStatBlock(character);
                Vector3 p = character._screenManager._cinematics._camera.WorldToScreenPoint(character.transform.localPosition);
                _statBlocks[character].transform.position = new Vector3(p.x, p.y - 60f, p.z);
            }            
        }
    }
    public void ReceiveEvent(GameEvent ge)
    {
        HandleEvent(ge);
    }
    public void HandleEvent(GameEvent ge)
    {
        if (ge is QuestEventAbility gea)
            HandleEventAbility(gea);
        else if (ge is QuestEventEffect ges)
            HandleEventEffect(ges);
    }

    public void HandleEventAbility(QuestEventAbility gea)
    {
        if (gea._ability is Action action)
        {
            if (gea._state == eActionState.Prepare)
            {
                _actionBlock[gea._ability] = Instantiate(_actionBlockPrefab, _actionCanvas.transform);
                _actionBlock[gea._ability].SetAction(action);
                Vector3 p = gea._actor._screenManager._cinematics._camera.WorldToScreenPoint(action._character.transform.localPosition);
                _actionBlock[gea._ability].transform.position = new Vector3(p.x, p.y + 75, p.z);
            }
            else if (gea._state == eActionState.Ready)
            {
                Destroy(_actionBlock[gea._ability].gameObject);
                _actionBlock.Remove(gea._ability);
            }
        }
    }

    public void HandleEventEffect(QuestEventEffect gea)
    {
        foreach (AbilityEffect effect in gea._ability._abilityData._effects)
        {
            EffectTip et = Instantiate(_effectTipPrefab, _textCanvas.transform);
            et.SetEffect(gea, effect);
        }
    }
}
