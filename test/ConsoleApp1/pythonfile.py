from naoqi import ALProxy
import sys
import math
import almath
import time

path = 'D:\\fyp\\test\\movement.txt'


#motion_service.Init()
while True :
    
    try:
        f= open(path, 'r')
        #all_words = map(lambda l: l.split(" "), f.readlines())
        all_words=f.read()
        f.close()
    except:
        continue

    print(all_words)
    time.sleep(3)




#x = int(sys.argv[1])
#y=1
#try:
    
#    y = int(sys.argv[2])
#except:
#    y=2


