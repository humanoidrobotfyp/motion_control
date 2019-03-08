from naoqi import ALProxy
import sys 
import time

path = 'D:\\fyp\\test\\movement.txt'

x=0
all_words = 0
robotIP="192.168.0.100"
motion_service = ALProxy("ALMotion",robotIP,9559)
posture_service = ALProxy("ALRobotPosture",robotIP,9559)
motion_service.setStiffnesses("Body",0)
posture_service.goToPosture("StandInit",0.5)
motion_service.setAngles("LShoulderPitch",x*3.14/180,0.35)
motion_service.setAngles("RShoulderPitch",x*3.14/180,0.35)
motion_service.setAngles("LElbowRoll",x*3.14/180,0.35)
motion_service.setAngles("RElbowRoll",x*3.14/180,0.35)
motion_service.setAngles("RShoulderRoll",x*3.14/180,0.35)
motion_service.setAngles("LShoulderRoll",x*3.14/180,0.35)
time.sleep(3)

while x<40:
    #motion_service.setAngles("LHipYawPitch",x*3.14/180,0.35)
    x=x+1
    time.sleep(0.1)
x=0
while x>-30:
    motion_service.setAngles("LHipYawPitch",x*3.14/180,0.35)
    x=x-1
    time.sleep(0.1)    
