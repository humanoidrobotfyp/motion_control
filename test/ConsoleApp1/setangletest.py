from naoqi import ALProxy
import sys 

path = 'D:\\fyp\\test\\movement.txt'

x=1
all_words = 0
'''robotIP="192.168.0.103"
motion_service = ALProxy("ALMotion",robotIP,9559)
posture_service = ALProxy("ALRobotPosture",robotIP,9559)
motion_service.setStiffnesses("Body",0)
posture_service.goToPosture("StandInit",0.5)'''
names = ['RShoulderPitch', 'RShoulderRoll','RElbowRoll','RElbowYaw','LShoulderPitch','LShoulderRoll','LElbowRoll','LElbowYaw']
time = 0.35
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
             break
        else :
            #motion_service.moveTo(0.03,0,0)
            if int(all_words[0])==1:
                angz=[float(all_words[1]) , float(all_words[2]),float(all_words[3]),0,float(all_words[5]),float(all_words[6]),float(all_words[7]),0]
               # motion_service.setAngles(names, angz, 0.35)
        print(all_words)
    except:
        continue
    


