#
#  Synthetic robotpy simulation to act
#  as datasource for UnitySim.
#
#  Send a single transform message for starters.
#
import udp_channels as udp
import json

import pdb; pdb.set_trace()

UNITY_SIM_IP = '127.0.0.1'
ROBOT_SIM_IP = '127.0.0.1'
chan = udp.UDPChannel(ROBOT_SIM_IP, 8001, UNITY_SIM_IP, 8002)

position = [ 1.0, 1.0, 1.0 ]
orientation = [ 1.0, 0.0 ]
transform = { "sender" : "synth frcpysim",
			  "message" : "transform",
			  "position" : position,
			  "orientation" : orientation
			}

message = json.dumps(transform)
chan.send_to(message)
