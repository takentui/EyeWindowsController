import cv2
import numpy as np
from matplotlib import pyplot as plt

class Part:
    def __init__ (self, path, size):
        self.cascade = cv2.CascadeClassifier(path)
        self.last = None
        self.remains = 0
        self.dump = True
        # Min pattern size
        self.size = size
        # Store last detected part for % frames
        self.memory = 10
    def __getitem__ (self, item):
        return self.last[item]
    # Save last seen part
    def setLast (self, last):
        self.last = last
        self.remains = self.memory
        return last
    # Restore last seen part
    def getLast (self):
        if self.remains:
            self.remains -= 1
            return self.last
        return None
    # Find part on frame
    def find (self, frame):
        parts = self.cascade.detectMultiScale(frame, 1.2, 2, 0, self.size)
        if self.dump:
            for (x, y, w, h) in parts:
                cv2.rectangle(frame, (x, y), (x + w, y + h), 0)
        # Try to restore last part
        if not len(parts):
            return self.getLast()
        parts = self.best(parts)
        if not len(parts):
            self.remains = 0
            self.last = None
            return None
        return self.setLast(parts[-1])
    # Select best available part
    def best (self, parts):
        return sorted(parts, key=lambda part: part[3] * part[2])
    # Draw rectangle around part
    def highlight (self, frame, color=255):
        if self.remains:
            (x, y, w, h) = self.last
            cv2.rectangle(frame, (x, y), (x + w, y + h), color)

class Face (Part):
    def __init__ (self, path):
        Part.__init__(self, path + 'haarcascade_frontalface_alt3.xml', (100, 100))
    def find (self, frame):
        result = Part.find(self, frame)
        # Extend face
        if self.remains is self.memory:
            self.last[3] *= 1.10
        return result

class Nose (Part):
    def __init__ (self, path, face):
        Part.__init__(self, path + 'haarcascade_mcs_nose.xml', (18, 15))
        self.face = face
    def best (self, parts):
        face = self.face.last
        r = face[3] * 0.8
        l = face[3] * 0.2
        return Part.best(self, [part for part in parts if l < part[1] and (part[1] + part[3]) < r])

class Mouth (Part):
    def __init__ (self, path, face):
        Part.__init__(self, path + 'haarcascade_mcs_mouth.xml', (25, 15))
        self.face = face
        self.up = 0.675
    def best (self, parts):
        face = self.face.last
        l = face[3] * self.up
        return Part.best(self, [part for part in parts if l < part[1] + part[3] / 2])

class Eyes (Part):
    def __init__ (self, path, face):
        Part.__init__(self, path + 'haarcascade_mcs_eyepair_big.xml', (90, 22))
        self.left = None
        self.left_pup = None
        self.right = None
        self.right_pup = None
        self.eyes = None
        self.memory *= 2
        self.face = face
    def pair (self, parts):
        for (ax, ay, aw, ah) in parts:
            return ((ax, ay, int(aw/2), ah), (int(ax + aw/2), ay, int(aw/2), ah))
        return None
    def pupil_detection(self, frame):
        # Поиск зрачков
        rows, cols = frame.shape
        # frame = cv2.medianBlur(frame,5)
        frame = cv2.adaptiveThreshold(frame,255,cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY,11,1)
        circles = cv2.HoughCircles(frame, cv2.HOUGH_GRADIENT, 2, 7, cols / 3,
                                   param1=30, param2=18, minRadius=0, maxRadius=0)
        print("circles = {0}".format(circles))
        if not circles is None:
            circles = np.uint16(np.around(circles))
            for i in circles[0, :]:
                cv2.circle(frame,(i[0],i[1]),i[2],(0,255,0), 2)
                return (i[0], i[1], i[2])
        return None

    def find (self, frame):
        eyes = None
        parts = self.cascade.detectMultiScale(frame, 1.2, 2, 0, self.size)
        h = self.face.last[3] / 2
        parts = self.pair(Part.best(self, [part for part in parts if part[1] + part[3] / 2 < h]))
        print("self.pair = {0}".format(parts))
        if parts is None:
            return self.getLast()
        return self.setLast(parts, frame)

    def setLast (self, parts, frame):
        parts = sorted(parts, key=lambda part: part[0])
        l1 = parts[0]
        r1 = parts[1]
        # if not self.left is None and not self.right is None:
        #     l0 = self.left
        #     r0 = self.right
        #     self.left_pup = self.pupil_detection(frame[l0[1]: l0[1] + l0[3], l0[0]:l0[0] + l0[2]])
        #     self.right_pup = self.pupil_detection(frame[r0[1]: r0[1] + r0[3], r0[0]:r0[0] + r0[2]])
        #     self.left = [(l0[0] + l1[0]) / 2, (l0[1] + l1[1]) / 2, (l0[2] + l1[2]) / 2, (l0[3] + l1[3]) / 2]
        #     self.right = [(r0[0] + r1[0]) / 2, (r0[1] + r1[1]) / 2, (r0[2] + r1[2]) / 2, (r0[3] + r1[3]) / 2]
        # else:
        self.left_pup = self.pupil_detection(frame[l1[1]: l1[1] + l1[3], l1[0]:l1[0] + l1[2]])
        self.right_pup = self.pupil_detection(frame[r1[1]: r1[1] + r1[3], r1[0]:r1[0] + r1[2]])
        self.left = parts[0]
        self.right = parts[1]
        self.eyes = [self.left, self.right]
        print("pupils = {0}, {1}".format(self.left_pup, self.right_pup))
        self.last = [
            ((self.left_pup[0] + self.right_pup[0]) + (self.left[0] + self.right[0])) / 2,
            ((self.left_pup[1] + self.right_pup[1]) + (self.left[1] + self.right[1])) / 2
        ]
        self.remains = self.memory
        return self.last
    def highlight (self, frame, color=150):
        if self.remains:
            x = self.face.last[0]
            y = self.face.last[1]
            # eye = [self.left, self.right]
            i = 0
            for (ex, ey, er) in [self.left_pup, self.right_pup]:
                nx = int(x + ex + self.eyes[i][0])
                ny = int(y + ey + self.eyes[i][1])
                cv2.circle(frame, (nx, ny), er, color, 2)
                i += 1
                # cv2.rectangle(frame, (int(nx - 1), int(ny - 1)), (int(nx + 1), int(ny + 1)), color)
            nx = x + self.last[0]
            ny = y + self.last[1]
            cv2.rectangle(frame, (int(nx - 1), int(ny - 1)), (int(nx + 1), int(ny + 1)), color)