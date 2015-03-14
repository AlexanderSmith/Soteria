using UnityEngine;
using System.Collections;

/*
 * As far as I can tell there may be a lot
 * of data to serialize their are actually very
 * little "Serializeable" objects, so enums will work
 * well here. Add enums as you see fit.
 */
public enum PersistentDataID
{
	E_MISSING_ID = 0,

	E_COOL_PLAYER_DATA
}