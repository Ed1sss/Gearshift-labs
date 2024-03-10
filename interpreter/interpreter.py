import serial
import pyvjoy
import time

acc_min_input = 0
acc_max_input = 1024
brk_min_input = 0
brk_max_input = 1024
whl_min_input = 0
whl_max_input = 1024

def calibrate():
    global acc_min_input, acc_max_input, brk_min_input, brk_max_input, whl_min_input, whl_max_input
    port = input("ENTER COM PORT: \n")
    ser = serial.Serial(port, 9600)
    print("CALIBRATING ACCELATOR")
    print("DURING 5 SECONDS PRESS TO THE MAX AND RELEASE TO LOWEST")
    acc_min_input, acc_max_input = get_min_max(ser, 0)
    print("CALIBRATING BRAKE")
    print("DURING 5 SECONDS PRESS TO THE MAX AND RELEASE TO LOWEST")
    brk_min_input, brk_max_input = get_min_max(ser, 1)
    print("CALIBRATING WHEEL")
    print("DURING 5 SECONDS STEER TO THE MAX AND BACK TO LOWEST")
    whl_min_input, whl_max_input = get_min_max(ser, 2)
    
def get_min_max(ser, input_num):
    min_value = 1024
    max_value = 0
    start_time = time.time()
    while True:
        elapsed_time = time.time() - start_time
        message = ser.readline()
        try:
            if elapsed_time >= 2:
                vstring = message.decode('utf-8').rstrip().lstrip().split(',')
                raw_input_value = int(vstring[input_num].rstrip().lstrip())
                print(raw_input_value)
                min_value = min(min_value, raw_input_value)
                max_value = max(max_value, raw_input_value)
        except Exception as e:
            print(f"Error: {e}")
        if elapsed_time >= 5:
            print(f"Minimum Value: {min_value}, Maximum Value: {max_value}")
            start_time = time.time()
            break
    pass
    return min_value, max_value
    
vjoy_min_ouput = 0
vjoy_max_ouput = 32768
def drive_mode():
    j = pyvjoy.VJoyDevice(1)
    port = input("ENTER COM PORT: \n")
    ser = serial.Serial(port, 9600)
    

    try:
        while True:
            message = ser.readline()
            try:
                vstring = message.decode('utf-8').rstrip().lstrip().split(',')
                y_ax = int(((int(vstring[0].rstrip().lstrip()) - acc_min_input) / (acc_max_input - acc_min_input)) * (vjoy_max_ouput - vjoy_min_ouput) + vjoy_min_ouput)# accelerator
                z_ax = int(((int(vstring[1].rstrip().lstrip()) - brk_min_input) / (brk_max_input - brk_min_input)) * (vjoy_max_ouput - vjoy_min_ouput) + vjoy_min_ouput)# brake
                x_ax = int(((int(vstring[2].rstrip().lstrip()) - whl_min_input) / (whl_max_input - whl_min_input)) * (vjoy_max_ouput - vjoy_min_ouput) + vjoy_min_ouput)# steering
                j.set_button(1, int(vstring[3])) #DOWN
                j.set_button(2, int(vstring[4])) #UP
                j.set_axis(pyvjoy.HID_USAGE_Z, int(z_ax))
                j.set_axis(pyvjoy.HID_USAGE_X, int(x_ax))
                j.set_axis(pyvjoy.HID_USAGE_Y, int(y_ax))
                print(vstring[0], vstring[1], vstring[2], vstring[3], vstring[4])
                print(y_ax, z_ax, x_ax)
            except Exception as e:
                print(f"Error: {e}")
    except KeyboardInterrupt:
        print("\nDrive mode interrupted. Returning to the main menu.")

def menu():
    while True:
        print("Select mode:")
        print("1. Calibration Mode")
        print("2. Drive Mode")
        print("3. Quit")

        choice = input("Enter your choice (1/2/3): ")

        if choice == "1":
            calibrate()
        elif choice == "2":
            drive_mode()
        elif choice == "3":
            break
        else:
            print("Invalid choice. Please enter 1, 2, or 3.")

menu()
