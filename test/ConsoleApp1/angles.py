from naoqi import ALProxy
import sys 

path = 'D:\\fyp\\test\\movement.txt'

x=1
all_words = 0
"""robotIP="192.168.0.105"
motion_service = ALProxy("ALMotion",robotIP,9559)
posture_service = ALProxy("ALRobotPosture",robotIP,9559)
motion_service.setStiffnesses("Body",0)
posture_service.goToPosture("StandInit",0.5)
motion_service.angleInterpolation("RShoulderPitch", 0, 0.6, True)
motion_service.angleInterpolation("RShoulderRoll", -1.32, 0.6, True)"""

while True :
    
    try:
        f= open(path, 'r')
        #all_words = map(lambda l: l.split(" "), f.readlines())
        words = f.readline()
        all_words = words.split()
        f.close()
    except:
        continue


    
    try:
        print(float(all_words[1])*180/3.14)
        if all_words=='exit' :
             break
        else:
            #motion_service.moveTo(0.03,0,0)
            #x=1
            #motion_service.setAngles("LShoulderRoll", float(all_words), 0.35)
            x=1                                 
    except:
         continue
    
    #Use one for single dimension array





#x = int(sys.argv[1])
#y=1
#try:
    
#    y = int(sys.argv[2])
#except:
#    y=2


