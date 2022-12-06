using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
   private void Update()
   {
      Cursor.lockState = CursorLockMode.Confined;
   }
}
