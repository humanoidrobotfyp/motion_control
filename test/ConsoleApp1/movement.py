from naoqi import ALProxy
import sys 

path = 'D:\\fyp\\test\\movement.txt'

x=1
all_words = 0
robotIP="192.168.0.105"
motion_service = ALProxy("ALMotion",robotIP,9559)
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
    try:
        if float(all_words)==9 :
             break
        if int(all_words[0])==1:
            motion_service.moveTo(0.03,0,0)
    except:
        continue
    
    #Use one for single dimension array





#x = int(sys.argv[1])
#y=1
#try:
    
#    y = int(sys.argv[2])
#except:
#    y=2


