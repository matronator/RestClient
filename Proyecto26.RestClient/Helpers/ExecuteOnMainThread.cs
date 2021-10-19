using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using UnityEngine;

namespace Proyecto26.Helper {

  /// <summary>
  /// Original solution from: https://stackoverflow.com/a/53818416/10305444
  /// Should also read: https://gamedev.stackexchange.com/a/195298/126092
  /// Usage:
  /// ```
  ///   ExecuteOnMainThread.RunOnMainThread.Enqueue(() => {
  ///     Code here will be called in the main thread...
  ///   });
  /// ```
  /// </summary>
  public class ExecuteOnMainThread: MonoBehaviour {

    /// <summary>
    /// Store all instance of Action's and try to invoke them
    /// </summary>
    public static readonly ConcurrentQueue < Action > RunOnMainThread = new ConcurrentQueue < Action > ();

    /// <summary>
    /// Unity's Update method
    /// </summary>
    void Update() {
      if (!RunOnMainThread.IsEmpty) {
        while (RunOnMainThread.TryDequeue(out var action)) {
          action?.Invoke();
        }
      }
    }
  }
}