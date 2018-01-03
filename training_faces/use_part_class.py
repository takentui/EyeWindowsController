import sys
import cv2
# import cv2.cv as cv
import os
import numpy as np
from face_parts import Face, Nose, Mouth, Eyes
from threading import Thread
from matplotlib import pyplot as plt

from time import time
path = "haar_cascades/"
# Load Haar cascades
cface = Face(path)
cnose = Nose(path, cface)
ceyes = Eyes(path, cface)
cmouth = Mouth(path, cface)
# Preview scale
scale = 2

# Head width history size
memory = 100
# Cursor history size
rmemory = 8
# Width queue
sQueue = []
# X cursor queue
xQueue = []
# Y cursor queue
yQueue = []
# capture = cv2.VideoCapture(0)
last = (0, 0)
lost = False
silent = False
sneak = False
if len(sys.argv) > 1:
    silent = True
def detect (frame, mvcc, k, mxc, myc):
    global last
    global lost
    size = (frame.shape[1], frame.shape[0])
    xsize = int(size[0] / scale)
    ysize = int(size[1] / scale)
    small = cv2.resize(frame, ((xsize, ysize)))
    temp = cv2.cvtColor(small, cv2.COLOR_BGR2GRAY)
    temp = cv2.flip(temp, 1)
    # Detect face
    face = cface.find(temp)
    if face is None:
        lost = True
        return (last, temp)
    lost = False
    (x, y, w, h) = face
    cface.highlight(temp)
    face = temp[y:y+h, x:x+w]
    # Detect eyes in thread
    te = Thread(target=Eyes.find, args=(ceyes, face))
    te.start()
    # Detect mouth in thread
    tm = Thread(target=Mouth.find, args=(cmouth, face))
    tm.start()
    # Detect nose
    nose = cnose.find(face)
    tm.join()
    te.join()
    eyes = ceyes.last
    mouth = cmouth.last
    # Draw rectangle around eyes
    if not sneak:
        ceyes.highlight(temp)
    if not mouth is None and not nose is None:
        nx = x + nose[0] + int(nose[2] / 2)
        ny = y + nose[1] + int(nose[3] / 2)
        mx = x + mouth[0] + int(mouth[2] / 2)
        my = y + mouth[1] + int(mouth[3] * mvcc)
        if not sneak:
            # Draw nose
            cv2.rectangle(temp, (mx - 1, my - 1), (mx + 1, my + 1), 255)
            # Draw mouth
            cv2.rectangle(temp, (nx - 1, ny - 1), (nx + 1, ny + 1), 255)
            # Mouth -> Nose
            cv2.line(temp, (mx, my), (nx, ny), 2, 0)

        if not eyes is None:
            print(eyes)
            ex = x + eyes[0]
            ey = y + eyes[1]
            cx = int(mx * (1 - k) + ex * k)
            cy = int(my * (1 - k) + ey * k)
            if not sneak:
                ex, ey = int(ex), int(ey)
                cx, cy = int(cx), int(cy)
                # Mouth -> Eyes
                cv2.line(temp, (int(mx), int(my)), (int(ex), int(ey)), 2, 0)
                # Nose -> Eyes
                print((nx, ny, ex, ey))
                cv2.line(temp, (nx, ny), (ex, ey), 2, 0)
                # Nose -> Center
                cv2.line(temp, (nx, ny), (cx, cy), 2, 0)

            sQueue.append(w)
            if len(sQueue) > memory:
                sQueue.pop(0)
            cursorScale = sum(sQueue) / len(sQueue)
            # Marker position
            rx = xsize / 2 + int((nx - cx) / mxc / cursorScale * xsize / 2)
            ry = ysize / 2 + int((ny - cy) / myc / cursorScale * ysize / 2)
            # Update cord queues
            yQueue.append(ry)
            xQueue.append(rx)
            if len(yQueue) > rmemory:
                yQueue.pop(0)
            if len(xQueue) > rmemory:
                xQueue.pop(0)
            # Get average x and y
            ry = sum(yQueue) / len(yQueue)
            rx = sum(xQueue) / len(xQueue)
            if rx < 4:
                rx = 4
            if ry < 4:
                ry = 4
            if rx > xsize - 4:
                rx = xsize - 4
            if ry > ysize - 4:
                ry = ysize - 4
            #cv2.rectangle(temp, (rx - 4, ry - 4), (rx + 4, ry + 4), 255)
            #cv2.rectangle(temp, (rx - 2, ry - 2), (rx + 2, ry + 2), 255)
            last = (int(rx * scale), int(ry * scale))
    return (last, temp)

# while True:
#     (success, frame) = capture.read()
#     (w, h) = (frame.shape[1], frame.shape[0])
#     if not success:
#         break
#
#     # c = cv2.WaitKey(1)
#     grid = np.zeros((h, w, 1), np.uint8);
#     ((x, y), screen) = detect(frame)
#     w3 = int(w / 3)
#     h3 = int(h / 3)
#     cv2.rectangle(grid, (0, h3), (w - 1, 2 * h3), 255)
#     cv2.rectangle(grid, (w3, 0), (2 * w3, h - 1), 255)
#     nx = int(x / w3)
#     ny = int(y / h3)
#     if lost:
#         print(-1, -1)
#     else:
#         print(x, y)
#
#     sys.stdout.flush()
#     if silent:
#         continue
#     # Highlight selected
#     cv2.rectangle(grid, (nx * w3, ny * h3), ((nx + 1) * w3, (ny + 1) * h3), 127, cv2.FILLED)
#     # Draw cursor
#     cv2.rectangle(grid, (x - 4, y - 4), (x + 4, y + 4), 0)
#     cv2.rectangle(grid, (x - 2, y - 2), (x + 2, y + 2), 0, cv2.FILLED)
#     cv2.imshow('grid', grid)
#     cv2.imshow('capture', screen)



if __name__ == '__main__':

    BLOCKS_COUNT = 9
    data = []
    for i in range(BLOCKS_COUNT):
        block_index = i + 1
        w, h = 640, 480
        grid = np.zeros((h, w, 1), np.uint8);
        block_data = []
        block_dir = os.walk('{0}'.format(block_index))
        for d, dirs, files in block_dir:
            # Пройдем по всем папкам
            for f in files:
                img = cv2.imread('{0}/{1}'.format(block_index, f))
                ((x, y), screen) = detect(img)
                w, h = 640, 480
                grid = np.zeros((h, w, 1), np.uint8);
                w3 = int(w / 3)
                h3 = int(h / 3)
                cv2.rectangle(grid, (0, h3), (w - 1, 2 * h3), 255)
                cv2.rectangle(grid, (w3, 0), (2 * w3, h - 1), 255)
                nx = int(x / w3)
                ny = int(y / h3)
                if lost:
                    print(-1, -1)
                else:
                    print("x={0}, y={1}".format(x, y))

                sys.stdout.flush()
                # if silent:
                #     continue
                # Highlight selected
                cv2.rectangle(grid, (nx * w3, ny * h3), ((nx + 1) * w3, (ny + 1) * h3), 127, cv2.FILLED)
                # Draw cursor
                cv2.rectangle(grid, (x - 4, y - 4), (x + 4, y + 4), 0)
                cv2.rectangle(grid, (x - 2, y - 2), (x + 2, y + 2), 0, cv2.FILLED)
                plt.imshow(screen)
                # Highlight selected
                cv2.rectangle(grid, (nx * w3, ny * h3), ((nx + 1) * w3, (ny + 1) * h3), 127, cv2.FILLED)
                # Draw cursor
                cv2.rectangle(grid, (x - 4, y - 4), (x + 4, y + 4), 0)
                cv2.rectangle(grid, (x - 2, y - 2), (x + 2, y + 2), 0, cv2.FILLED)
                cv2.imshow('grid', grid)
                plt.show()
