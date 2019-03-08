from naoqi import ALProxy
import sys 
import time

path = 'D:\\fyp\\test\\movement.txt'

x=0.5
y=0.0
theta = 0.0
frequency = 0.9

all_words = 0
robotIP="192.168.0.104"
motion_service = ALProxy("ALMotion",robotIP,9559)
posture_service = ALProxy("ALRobotPosture", robotIP, 9559)
posture_service.goToPosture("StandInit", 1)
print (all_words)
motion_service.moveInit()
motion_service.moveToward(x, y, theta)
time.sleep(20)
motion_service.stopMove()
