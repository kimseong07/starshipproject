﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.GetComponent<ShipMove>() != null) {
            Destroy(gameObject);
        }
    }
}
