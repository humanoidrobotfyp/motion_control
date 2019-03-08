from naoqi import ALProxy
import sys 

path = 'D:\\fyp\\test\\movement.txt'

x=1
all_words = 0
robotIP="192.168.0.105"
motion_service = ALProxy("ALMotion",robotIP,9559)
posture_service = ALProxy("ALRobotPosture",robotIP,9559)
motion_service.setStiffnesses("Body",0)
posture_service.goToPosture("StandInit",0.5)
names = ['RShoulderPitch', 'RShoulderRoll','RElbowRoll',]# , 'RWristYaw', 'RHand']
time = 0.35
times = [ [time], [time], [time]]# [time], [time], [time] ]
#motion_service.Init()
while True :
    
    try:
        f= open(path, 'r')
        words = f.readline()
        all_words = words.split()
        f.close()
    except:
        continue


    print(all_words)
    try:
        if all_words=='exit' :
             break
        else :
            #motion_service.moveTo(0.03,0,0)
            if int(all_words[0])==1:
                angz=[float(all_words[1]) , float(all_words[2]),0]
                motion_service.angleInterpolation(names, angz, times, True)
    except:
        continue
    
    #Use one for single dimension array





#x = int(sys.argv[1])
#y=1
#try:
    
#    y = int(sys.argv[2])
#except:
#    y=2


