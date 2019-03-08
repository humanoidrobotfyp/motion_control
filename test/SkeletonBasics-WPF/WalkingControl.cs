using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using System.Windows.Media.Media3D;


namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    class WalkingControl
    {
        double prevLeftKneeHeight;
        double prevRightKneeHeight;
        double status;
        int framecounter;
        int prevWalkingStatus;

        public WalkingControl()
        {
            status = 0;
        }

        private double GetYValue(Skeleton skeleton,string LeftorRight)
        {
           
            if(LeftorRight=="Left")
            {
                return skeleton.Joints[JointType.KneeLeft].Position.Y;
            }
            else
            {
                return skeleton.Joints[JointType.KneeRight].Position.Y;
            }
            
        }

        public void SetFirstThreshold(Skeleton skeleton)
        {
            prevLeftKneeHeight = GetYValue(skeleton,"Left");
            prevRightKneeHeight= GetYValue(skeleton,"Right");
            status = 0;
            framecounter = 0;
            prevWalkingStatus =1;
        }

        public void CheckStatus(Skeleton skeleton)
        {
            double LeftKneeHeight = GetYValue(skeleton, "Left");
            double RightKneeHeight = GetYValue(skeleton, "Right");

            if (Math.Abs(LeftKneeHeight-prevLeftKneeHeight) >= 0.05 || Math.Abs(RightKneeHeight-prevRightKneeHeight) >= 0.05)
            {
                prevLeftKneeHeight = LeftKneeHeight;
                prevRightKneeHeight = RightKneeHeight;

                if (prevWalkingStatus == 0)
                {
                    status = 1;
                    prevWalkingStatus = 1;
                }

                else if (prevWalkingStatus == 1)
                {
                    status = 2;
                }

                framecounter = 0;
            }

            else
            {
                if( framecounter >=12 )
                {
                    status = 0;
                    prevWalkingStatus = 0;
                    framecounter = 0;
                }

                else
                {
                    framecounter++;
                }

            }

        }

        public string GetStatus()
        {
            return status.ToString();
        }
    }
}
