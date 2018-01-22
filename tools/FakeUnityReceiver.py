# FakeUnityReceiver.py
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
unity = udp.UDPChannel(UNITY_SIM_IP, UNITY_LISTEN_PORT, 
					  ROBOT_SIM_IP, udp.UDPChannel.default_local_port)

# see what Unity would receive
x = 1.0
while True:

	#  If it times out, we get None, None
	unity_msg, sender = unity.receive_from()

	if unity_msg is not None:
		print("Received: " + unity_msg)
		print("From: ", sender)
		unity_msg_obj = json.loads(unity_msg)
		if unity_msg_obj is not None:
			if unity_msg_obj["position"][0] == x:
				print("got the expected increasing x position")
		else:
			print("Failed to decode message!")

		x = x + 1.0
	else:
		print("Timed out, try again, in a bit")
		time.sleep(1.0)
