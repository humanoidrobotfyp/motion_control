from naoqi import ALProxy
import sys 

path = 'D:\\fyp\\test\\movement.txt'

x=1
all_words = 0
robotIP="192.168.0.100"
motion_service = ALProxy("ALMotion",robotIP,9559)
posture_service = ALProxy("ALRobotPosture",robotIP,9559)
motion_service.setStiffnesses("Body",0)
posture_service.goToPosture("StandInit",0.5)
names = ['RShoulderPitch', 'RShoulderRoll','RElbowRoll','RElbowYaw','LShoulderPitch','LShoulderRoll','LElbowRoll','LElbowYaw','LHipYawPitch']
time = 0.35
x=0.65
y=-0.03
theta = 0.0
frequency = 0.5

#motion_service.Init()
while True :
    
    try:
        f= open(path, 'r')
        words = f.readline()
        all_words = words.split()
        f.close()
    except:
        continue


    
    try:
        if all_words[0]=='exit' :
             motion_service.stopMove()
             
             break
        else :
            
            if int(all_words[0])==1:
                angz=[float(all_words[1]) , float(all_words[2]),float(all_words[3]),float(all_words[4]),float(all_words[5]),float(all_words[6]),float(all_words[7]),float(all_words[8]),0]
                motion_service.setAngles(names, angz, 0.35)
                
                if all_words[10]=='0':
                    motion_service.stopMove()
                    print(float(all_words[10]))    
                elif all_words[10]=='1':
                    #motion_service.moveInit()
                    motion_service.moveToward(x, y, theta)
                    
        #print(all_words[3])
            else:
                motion_service.stopMove()
    except:
        continue
    
    #Use one for single dimension array





#x = int(sys.argv[1])
#y=1
#try:
    
#    y = int(sys.argv[2])
#except:
#    y=2


