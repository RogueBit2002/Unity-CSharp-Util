using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LaurensKruis.CSharpUtil
{
    public class WaitForParallelCoroutines : CustomYieldInstruction
    {

        private Dictionary<int, bool> states = new Dictionary<int, bool>();

        public WaitForParallelCoroutines(IEnumerable<IEnumerator> enumerators, Func<IEnumerator, Coroutine> coroutineStarter)
        {
            int index = 0;
            foreach (IEnumerator enumerator in enumerators)
            {
                states.Add(index, false);
                coroutineStarter(StatefulCoroutine(enumerator, index));
                index++;
            }
        }


        public override bool keepWaiting 
        {
            get
            {
                foreach (bool value in states.Values)
                    if (!value)
                        return true;

                return false;
            }
        }

        private IEnumerator StatefulCoroutine(IEnumerator enumerator, int index)
        {
            yield return enumerator;
            states[index] = true;
        }
    }
}
