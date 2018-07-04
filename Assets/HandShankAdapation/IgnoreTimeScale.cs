using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.HandShankAdapation
{
    static public  class  IgnoreTimeScale
    {
        public static IEnumerator Play( this Animation animation, string clipName, bool useTimeScale, Action onComplete )
        {
           
            //We Don't want to use timeScale, so we have to animate by frame..
            if(!useTimeScale)
            {
                AnimationState currState = animation[clipName];
                bool isPlaying = true;
                float startTime = 0F,
                        progressTime = 0F,
                        timeAtLastFrame = 0F,
                        timeAtCurrentFrame = 0F,
                        deltaTime = 0F;


                animation.Play(clipName);
  
                timeAtLastFrame = Time.realtimeSinceStartup;
                while (isPlaying) 
                {
                    timeAtCurrentFrame = Time.realtimeSinceStartup;
                    deltaTime = timeAtCurrentFrame - timeAtLastFrame;
                    timeAtLastFrame = timeAtCurrentFrame; 
  
                    progressTime += deltaTime;
                    currState.normalizedTime = progressTime / currState.length; 
                    animation.Sample ();
  
                    //Debug.Log(_progressTime);
  
                    if (progressTime >= currState.length) 
                    {
                        //Debug.Log(&quot;Bam! Done animating&quot;);
                        if(currState.wrapMode != WrapMode.Loop)
                        {
                            //Debug.Log(&quot;Animation is not a loop anim, kill it.&quot;);
                            //_currState.enabled = false;
                            isPlaying = false;
                        }
                        else
                        {
                            //Debug.Log(&quot;Loop anim, continue.&quot;);
                            progressTime = 0.0f;
                        }
                    }
  
                    yield return new WaitForEndOfFrame();
                }
                yield return null;
                if(onComplete != null)
                {
                    onComplete();
                } 
            }
            else
            {
                animation.Play(clipName);
            }
        }
    }
}
