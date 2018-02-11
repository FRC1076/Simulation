#
#  Synthetic robotpy simulation to act
#  as datasource for UnitySim.
#
#  Send a single transform message for starters.
#
import udp_channels as udp
import json
import time

# set IP address accordingly.  (could be loopback if on the same host)
UNITY_SIM_IP = '127.0.0.1'
UNITY_LISTEN_PORT = 8000
ROBOT_SIM_IP = '127.0.0.1'
chan = udp.UDPChannel(ROBOT_SIM_IP, udp.UDPChannel.default_local_port,
					  UNITY_SIM_IP, UNITY_LISTEN_PORT)



a = 1

x = 1.0
y = 0.0 #Dont change, we dont want the robot to fly!
z = 1.0

# move along 9 units 1 second at a time
while a < 10.0:

	position = [ x, y, z ]
	orientation = [ 1.0, 0.0 ]
	transform = { "sender" : "synthetic robotpy",
		"receiver" : "robot-1",	
		"message" : "transform",
		"position" : position,
		"orientation" : orientation
	}

	message = json.dumps(transform)
	print("Sending: {}".format(message))
	chan.send_to(message)

	# move a bit and wait for a second
	a += 1.0
	time.sleep(1.0)
