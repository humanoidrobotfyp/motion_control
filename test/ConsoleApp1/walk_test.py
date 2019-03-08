from naoqi import ALProxy
import sys 
import time

path = 'D:\\fyp\\test\\movement.txt'

x=0.5
y=0.0
theta = 0.0
frequency = 0.9

all_words = 0
robotIP="192.168.0.105"
motion_service = ALProxy("ALMotion",robotIP,9559)
posture_service = ALProxy("ALRobotPosture", robotIP, 9559)
posture_service.goToPosture("StandInit", 0.5)
while True :
    try:
        f= open(path, 'r')
        words = f.readline()
        all_words = words.split()
        f.close()
        
    except:
        continue
    print (all_words)
    try:
        if all_words[0]=='0':
            motion_service.stopMove()
            continue
        if all_words[0]=='exit' :
             break
        elif float(all_words[1])== 1:
            motion_service.moveToward(x, y, theta)
        elif all_words[1]=='0' :
            motion_service.stopMove()
    except:
        continue
    


