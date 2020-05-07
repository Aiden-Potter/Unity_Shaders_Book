using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> where T :new() {
      private static T instance;
 
     private static readonly object locker = new object();
  
      public static T Instance
      {
          get
         {
             lock (locker)
             {
                 if (instance == null)
                     instance = new T();
                 return instance;
             }
         }
     }
}
