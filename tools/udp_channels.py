import socket
#
#  Create the infra for two-way communication channel using UDP
#  Set receive from timeout to .001 seconds to avoid blocking for
#  long.
#
#  Revised for python3 (requires decoding and encoding string
#  automatically so user doesn't have to deal with it.
#
#
def test_basic_function():
    """Do basic back and forth to make sure things work"""
    loc_chan = UDPChannel()
    rem_chan = UDPChannel(local_port=UDPChannel.default_remote_port,
                        remote_port=UDPChannel.default_local_port)

    # test basic send/receive (and string encode/decode)
    loc_chan.send_to("hello from local")
    reply, sender = rem_chan.receive_from()
    assert(reply == "hello from local")

    # and the reverse just to be sure
    rem_chan.send_to("hello from remote")
    reply, sender = loc_chan.receive_from()
    assert(reply == "hello from remote")

    # test timeout
    reply, sender = loc_chan.receive_from()
    assert(reply == None and sender == (None,None))


class UDPChannel:
        """
        Create a communication channel to send and receive messages
        between two addresses and ports.
        Defaults are loopback address with specific port address.
        timeout_in_seconds is the receive_from time out value.
        """
        default_local_port = 5888
        default_remote_port = 5880
        # Useful defaults permit minimal arguments for simple test.
        # On one end:
        #      sender = UDPChannel()
        #    receiver = UDPChannel(local_port=sender.remote_port, remote_port=sender.local_port)
        def __init__(self,
                     local_ip="127.0.0.1",
                     local_port=default_local_port,
                     remote_ip="127.0.0.1",
                     remote_port=default_remote_port,
                     timeout_in_seconds=0.001, receive_buffer_size=8192):
                """Create the sending and receiving sockets for a communcation channel"""
                self.local_ip = local_ip
                self.local_port = local_port
                self.remote_ip = remote_ip
                self.remote_port = remote_port

                # create the receive socket
                self.receive_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
                self.receive_socket.bind((local_ip, local_port))

                # and the sending socket
                self.send_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

                # cache other configurable parameters
                self.timeout_in_seconds = timeout_in_seconds
                self.receive_buffer_size = receive_buffer_size

        def send_to(self, message):
                """send message to the other end of the channel"""
                self.send_socket.sendto(message.encode(), (self.remote_ip, self.remote_port))

        def receive_from(self):
                """wait for timeout to receive a message from channel"""
                self.receive_socket.settimeout(self.timeout_in_seconds)
                try:
                    reply, server_address_info = self.receive_socket.recvfrom(self.receive_buffer_size)
                    return reply.decode(), server_address_info
                except socket.timeout:
                    return None, (None,None)

