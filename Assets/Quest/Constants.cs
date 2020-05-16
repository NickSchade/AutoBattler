using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eCharacterAnimation { Walking, Standing, MeleeStartup, MeleeStrike, MeleeFollowThrough};
public enum eActionState { Prepare, Act, Lag, Ready, Instant, Cancelled};
public enum eFormation { None, Assemble, Move, Engage};
public enum eWeapon { None, Sword, Dagger};