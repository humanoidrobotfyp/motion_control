using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using Microsoft.Kinect;

namespace Microsoft.Samples.Kinect.SkeletonBasics
{
    class AnglesCalculator
    {
        private Vector3D LeftShoulder;
        private Vector3D RightShoulder;
        private Vector3D LeftElbow;
        private Vector3D RightElbow;
        private Vector3D LeftWrist;
        private Vector3D RightWrist;
        private Vector3D PrevRightWrist;
        private Vector3D PrevRightElbow;
        private Vector3D Neck;
        private Vector3D HipCenter;
        private double angle;
        private double RShoulderPitch,RShoulderRoll,RElbowRoll,PREyaw,PLEyaw,counterL,counterR;
        private double LShoulderPitch,LShoulderRoll,LElbowRoll;

        private Vector3D ToVector(Microsoft.Kinect.SkeletonPoint Point)
        {
            return new Vector3D(Point.X, Point.Y, Point.Z);
        }

        private Vector3D ComputeRotations(Vector3D V, double pitch, double roll)
        {
            double[,] Matrix = new double[,]
            {
                {   cos(roll)               ,        0          ,              sin(roll)       },
                {   sin(roll)*sin(pitch)    ,    cos(pitch)     ,     -cos(roll)*sin(pitch)    },
                {   -sin(roll)*cos(pitch)   ,    sin(pitch)     ,        cos(roll)*cos(pitch)  }
            };

            double[] finalvec = new double[] { 0, 0, 0 };
            double[] initialvec = new double[] { V.X, V.Y, V.Z };
            for (int i = 0; i < 3; i++)
            {
                for(int j=0; j<3 ; j++)
                {
                    finalvec[i] = finalvec[i] + (Matrix[i, j] * initialvec[j]);
                }
            }


            return new Vector3D(finalvec[0], finalvec[1], finalvec[2]);
        }

        private double cos(double angle)
        {
            return Math.Cos(angle);
        }

        private double sin(double angle)
        {
            return Math.Sin(angle);
        }

        public AnglesCalculator()
        {
            PREyaw = 0;
            PLEyaw = 0;
            counterL = 0;
            counterR = 0;
        }

        public void FirstTime(Skeleton skeleton)
        {
            PrevRightElbow = ToVector(skeleton.Joints[JointType.ElbowRight].Position);
            PrevRightWrist= ToVector(skeleton.Joints[JointType.WristRight].Position);
        }

        public void SetJoints(Skeleton skeleton)
        {
            LeftShoulder= ToVector(skeleton.Joints[JointType.ShoulderLeft].Position);
            RightShoulder = ToVector(skeleton.Joints[JointType.ShoulderRight].Position);
            LeftElbow = ToVector(skeleton.Joints[JointType.ElbowLeft].Position);
            RightElbow = ToVector(skeleton.Joints[JointType.ElbowRight].Position);
            LeftWrist = ToVector(skeleton.Joints[JointType.WristLeft].Position);
            RightWrist = ToVector(skeleton.Joints[JointType.WristRight].Position);
            HipCenter = ToVector(skeleton.Joints[JointType.HipCenter].Position);
            Neck = ToVector(skeleton.Joints[JointType.ShoulderCenter].Position);
            angle = 0;
            
        }

        private double InnerProduct(Vector3D vectorA, Vector3D vectorB)
        {
            double dotProduct;
            vectorA.Normalize();
            vectorB.Normalize();
            dotProduct = Vector3D.DotProduct(vectorA, vectorB);

            return (double)Math.Acos(dotProduct);
        }

        private string RightShoulderPitch()
        {
            var shtemp = RightShoulder.Z;
            var eltemp = RightElbow.Z;
            

            if (Math.Abs(RightShoulder.Z-RightElbow.Z)<0.1 && Math.Abs(RightShoulder.Y-RightElbow.Y)<0.3)
            {
                shtemp = 1.0f;
                eltemp = 0.9f;
            }
            if (RightShoulder.Y < RightElbow.Y || (eltemp>shtemp && RightWrist.Y>= RightElbow.Y))
            {
                angle = Math.Atan(Math.Abs(RightShoulder.Y - RightElbow.Y) / Math.Abs(shtemp - eltemp));
                angle = -(angle);
                if (angle < -2.0)
                {
                    angle = -2.0;
                }

            }
            else
            {
                angle = Math.Atan((shtemp - eltemp) / (RightShoulder.Y - RightElbow.Y));
                angle = Math.PI / 2 - angle;
            }
            RShoulderPitch = angle;
            return angle.ToString();
        }

        private string LeftShoulderPitch()
        {
            var shtemp = LeftShoulder.Z;
            var eltemp = LeftElbow.Z;


            if (Math.Abs(LeftShoulder.Z - LeftElbow.Z) < 0.1 && Math.Abs(LeftShoulder.Y - LeftElbow.Y) < 0.3)
            {
                shtemp = 1.0f;
                eltemp = 0.9f;
            }
            if (LeftShoulder.Y < LeftElbow.Y || (LeftElbow.Z > LeftShoulder.Z && LeftWrist.Y >= LeftElbow.Y))
            {
                angle = Math.Atan(Math.Abs(LeftShoulder.Y - LeftElbow.Y) / Math.Abs(shtemp - eltemp));
                angle = -(angle);
                if (angle < -2.0)
                {
                    angle = -2.0;
                }

            }
            else

            {
                angle = Math.Atan((shtemp- eltemp) / (LeftShoulder.Y - LeftElbow.Y));
                angle = Math.PI / 2 - angle;
            }
            LShoulderPitch = angle;

            return angle.ToString();
        }

        private string RightShoulderRoll()
        {
            var tempshoulder = RightShoulder.Z;
            var tempelbow = RightElbow.Z;

            if (RightShoulder.Z < RightElbow.Z)
            {
                var tempx = tempshoulder;
                tempshoulder = tempelbow;
                tempelbow = tempx;

            }

            if (tempshoulder - tempelbow < 0.1)
            {
                tempshoulder = 1.0f;
                tempelbow = 0.9f;
            }

            angle = Math.Atan((RightShoulder.X - RightElbow.X) / (tempshoulder - tempelbow));

            RShoulderRoll = angle;

            angle = angle + 10 * Math.PI / 180;

            return angle.ToString();
        }

        private string LeftShoulderRoll()
        {
            var tempshoulder = LeftShoulder.Z;
            var tempelbow = LeftElbow.Z;
            if (LeftShoulder.Z < LeftElbow.Z)
            {
                var tempx = tempelbow;
                tempelbow = tempshoulder;
                tempshoulder = tempx;
            }


            if (tempshoulder - tempelbow < 0.1)
            {
                tempshoulder = 1.0f;
                tempelbow = 0.9f;
            }

            angle = Math.Atan((LeftShoulder.X - LeftElbow.X) / (tempshoulder-tempelbow));

            if(angle > 76*Math.PI/180)
            {
                angle = 76 * Math.PI / 180;
            }

            LShoulderRoll = angle;

            angle = angle - 10 * Math.PI / 180;
            return angle.ToString();
        }

        private string LeftElbowRoll()
        {
            angle = InnerProduct(LeftElbow - LeftShoulder, LeftElbow - LeftWrist) - Math.PI;
            if (angle < -88.5 * Math.PI / 180)
            {
                angle = -88.5 * Math.PI / 180;
            }
            LElbowRoll = angle;
            return angle.ToString();


        }

        private string RightElbowRoll()
        {
            angle = Math.PI - InnerProduct(RightElbow - RightShoulder, RightElbow - RightWrist);
            
            RElbowRoll = angle;

            return angle.ToString();
        }

        private string RightElbowYaw()
        {

            if (Math.Abs(RElbowRoll) < 20 * Math.PI / 180)
            {
                angle = 0;
                PREyaw = 0;
                counterR = 0;
                return angle.ToString();
            }

            Vector3D V1 = RightWrist - RightElbow;
            V1.Normalize();


            double deltax = V1.X;
            double deltay = V1.Y;
            double X, Y, Z;


            Z = V1.X * Math.Sin(RShoulderRoll) + V1.Z * Math.Cos(RShoulderRoll);
            Y = V1.Y;
            X = V1.X * Math.Cos(RShoulderRoll) - V1.Z * Math.Sin(RShoulderRoll);


            Z = V1.Y * Math.Sin((RShoulderPitch)) + Z * Math.Cos((RShoulderPitch));
            Y = V1.Y * Math.Cos((RShoulderPitch)) - Z * Math.Sin((RShoulderPitch));
            X = X;


            deltax = X * Math.Sin(RElbowRoll);

            angle = Math.Atan(Y / Math.Abs(deltax));

        
            if (deltax > 0)
            {
                angle = Math.PI - angle;
            }

            if(Math.Abs(RShoulderRoll) > 60 * Math.PI / 180)
            {
                angle = Math.Atan((RightWrist.Y - RightElbow.Y) / Math.Abs(RightElbow.Z - RightWrist.Z));
                if (RightWrist.Z > RightElbow.Z)
                {
                    angle = Math.PI - angle;
                }

                if (RightElbow.Y >= RightShoulder.Y && RShoulderPitch <= -75 * Math.PI / 180)
                {
                   angle = angle - Math.PI / 2;
                }
            }

             if((Math.Abs(angle-PREyaw)*180/Math.PI <=25) || counterR>=5 ||PREyaw==0)
            {
                PREyaw = angle;
                counterR = 0;
            }
            else
            {
                counterR++;
                angle = PREyaw;
            }

            //angle = -angle;
            return angle.ToString();

        }

        private string LeftElbowYaw()
        {

            if (Math.Abs(LElbowRoll) < 20 * Math.PI / 180)
            {
                angle = 0;
                PLEyaw = 0;
                counterL = 0;
                return angle.ToString();
            }

            Vector3D V1 = LeftWrist - LeftElbow;
            V1.Normalize();

            double deltax = V1.X;
            double deltay = V1.Y;
            double X, Y, Z;


            Z = V1.X * Math.Sin(LShoulderRoll) + V1.Z * Math.Cos(LShoulderRoll);
            deltay = V1.Y;
            X = V1.X * Math.Cos(LShoulderRoll) - V1.Z * Math.Sin(LShoulderRoll);


            Z = V1.Y * Math.Sin((LShoulderPitch)) + Z * Math.Cos((LShoulderPitch));
            Y = V1.Y * Math.Cos((LShoulderPitch)) - Z * Math.Sin((LShoulderPitch));
            X = X;


            deltax = -X * Math.Sin(LElbowRoll);

            angle = Math.Atan(Y / Math.Abs(deltax)); 
            

            if (deltax<0 )
            {
                 angle = Math.PI - angle;
            }

            if ( LShoulderRoll > 65 * Math.PI / 180 )
            {
                angle = Math.Atan((LeftWrist.Y - LeftElbow.Y) / Math.Abs(LeftElbow.Z - LeftWrist.Z));
                if(LeftWrist.Z>LeftElbow.Z)
                {
                    angle = Math.PI - angle;
                }

                if(LeftElbow.Y>=LeftShoulder.Y && LShoulderPitch<=-75*Math.PI/180)
                {
                    angle = angle - Math.PI / 2;
                }

            }

            if ((Math.Abs(angle - PLEyaw) * 180 / Math.PI <= 25) || counterL >= 5 || PLEyaw == 0)
            {
                PLEyaw = angle;
                counterR = 0;
            }
            else
            {
                counterL++;
                angle = PLEyaw;
            }

            

            angle = -angle;
             return angle.ToString();
            
        }

        private double Torso()
        {
            angle = Math.Atan( (Neck.Z - HipCenter.Z) / (Neck.Y - HipCenter.Y));
            if(angle < -25*Math.PI/180)
            {
                angle = -25 * Math.PI / 180;
            }
            else if(angle > 10 *Math.PI/180)
            {
                angle = 10 * Math.PI / 180;
            }

            return angle;
        }

        public string GetAllAngles()
        {
            string Allangles = RightShoulderPitch() + " " + RightShoulderRoll() + " " + RightElbowRoll() + " " + RightElbowYaw() + " " 
                + LeftShoulderPitch() + " " + LeftShoulderRoll() + " " + LeftElbowRoll()+" "+ LeftElbowYaw() + " "+ Torso().ToString();              //Left Upper Body
            return Allangles;
        }

        public string GetAngle()
        {
            //To Be Used to Get a Single Angle For Testing Purposes Only
            return "1"; //change here
        }
    }
}
