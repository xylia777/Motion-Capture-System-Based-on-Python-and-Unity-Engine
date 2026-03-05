#输出的数据效果最理想的版本  且没有特别严重的兼容问题
import cv2
import mediapipe as mp
import socket

# 初始化 UDP 套接字
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5054)

# 初始化 MediaPipe 的姿态检测模块
mp_pose = mp.solutions.pose
pose = mp_pose.Pose()

# 初始化视频捕获
cap = cv2.VideoCapture("5.mp4")
posList = []

while True:
    success, img = cap.read()
    if not success:
        print("格式不正确！")
        break

    # 将图像转换为 RGB 格式
    img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

    # 进行姿态检测
    results = pose.process(img_rgb)

    lmList = []
    if results.pose_landmarks:
        for id, lm in enumerate(results.pose_landmarks.landmark):
            h, w, c = img.shape
            cx, cy = int(lm.x * w), int(lm.y * h)
            lmList.append([id, cx, cy, int(lm.visibility * 100)])

    if lmList:
        lmString = ''
        for lm in lmList:
            lmString += f'{lm[1]},{img.shape[0] - lm[2]},{lm[3]},'
        posList.append(lmString)
        print(posList)
        # 发送数据
        data = lmString
        sock.sendto(str.encode(str(data)), serverAddressPort)

    # 绘制姿态关键点
    if results.pose_landmarks:
        mp.solutions.drawing_utils.draw_landmarks(
            img, results.pose_landmarks, mp_pose.POSE_CONNECTIONS)

    # 创建一个指定大小的窗口，这里设置窗口大小
    cv2.namedWindow('Image', cv2.WINDOW_NORMAL)
    cv2.resizeWindow('Image', 500, 620)
    cv2.imshow("Image", img)

    key = cv2.waitKey(1)

    # 记录数据到本地
    with open("MotionData.txt", 'w') as f:
        f.writelines(["%s\n" % item for item in posList])

    if (key==27 or key == ord('q')):
        break

# 释放资源
cap.release()
cv2.destroyAllWindows()
sock.close()
