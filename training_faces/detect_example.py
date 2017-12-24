import sys
import cv2
import cv2.cv as cv
import numpy as np
from parts import Face, Nose, Mouth, Eyes
from threading import Thread
from time import time
path = "cascades/"
# Load Haar cascades
cface = Face(path)
cnose = Nose(path, cface)
ceyes = Eyes(path, cface)
cmouth = Mouth(path, cface)
# Preview scale
scale = 2

# Mouth vert. center correction
mvcc = 0.3
# Main triangle ratio
k = 0.5
# X correction
mxc = 0.05
# Y correction
myc = 0.035
# Head width history size
memory = 100
# Cursor history size
rmemory = 8
# Width queue
sQueue = []
95
# X cursor queue
xQueue = []
# Y cursor queue
yQueue = []
capture = cv2.VideoCapture(0)
last = (0, 0)
lost = False
silent = False
sneak = True
if len(sys.argv) > 1:
silent = True
def detect (frame):
global last
global lost
96
size = (frame.shape[1], frame.shape[0])
xsize = int(size[0] / scale)
ysize = int(size[1] / scale)
small = cv2.resize(frame, ((xsize, ysize)))
temp = cv2.cvtColor(small, cv.CV_BGR2GRAY)
temp = cv2.flip(temp, 1)
# Detect face
face = cface.find(temp)
if face is None:
lost = True
return (last, temp)
lost = False
(x, y, w, h) = face
cface.highlight(temp)
97
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
98
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
ex = x + eyes[0]
99
ey = y + eyes[1]
cx = int(mx * (1 - k) + ex * k)
cy = int(my * (1 - k) + ey * k)
if not sneak:
# Mouth -> Eyes
cv2.line(temp, (mx, my), (ex, ey), 2, 0)
# Nose -> Eyes
cv2.line(temp, (nx, ny), (ex, ey), 2, 0)
# Nose -> Center
cv2.line(temp, (nx, ny), (cx, cy), 2, 0)
sQueue.append(w)
if len(sQueue) > memory:
sQueue.pop(0)
cursorScale = sum(sQueue) / len(sQueue)
100
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
101
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
while True:
(success, frame) = capture.read()
(w, h) = (frame.shape[1], frame.shape[0])
if not success:
break
102
c = cv.WaitKey(1)
grid = np.zeros((h, w, 1), np.uint8);
((x, y), screen) = detect(frame)
w3 = int(w / 3)
h3 = int(h / 3)
cv2.rectangle(grid, (0, h3), (w - 1, 2 * h3), 255)
cv2.rectangle(grid, (w3, 0), (2 * w3, h - 1), 255)
nx = int(x / w3)
ny = int(y / h3)
if lost:
print -1, -1
else:
print x, y
103
sys.stdout.flush()
if silent:
continue
# Highlight selected
cv2.rectangle(grid, (nx * w3, ny * h3), ((nx + 1) * w3, (ny + 1) * h3), 127, cv.CV_FILLED)
# Draw cursor
cv2.rectangle(grid, (x - 4, y - 4), (x + 4, y + 4), 0)
cv2.rectangle(grid, (x - 2, y - 2), (x + 2, y + 2), 0, cv.CV_FILLED)
cv2.imshow('grid', grid)
cv2.imshow('capture', screen)
import cv2
class Part:
def __init__ (self, path, size):
self.cascade = cv2.CascadeClassifier(path)
self.last = None
self.remains = 0
104
self.dump = False
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
105
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
106
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
Part.__init__(self, path + 'haarcascade_frontalface_default.xml', (100, 100))
def find (self, frame):
107
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
return Part.best(self, [
108
part for part in parts if l < part[1] and (part[1] + part[3]) < r
])
class Mouth (Part):
def __init__ (self, path, face):
Part.__init__(self, path + 'haarcascade_mcs_mouth.xml', (25, 15))
self.face = face
self.up = 0.675
def best (self, parts):
face = self.face.last
l = face[3] * self.up
return Part.best(self, [
part for part in parts if l < part[1] + part[3] / 2
])
class Eyes (Part):
def __init__ (self, path, face):
109
Part.__init__(self, path + 'haarcascade_eye.xml', (10, 10))
self.left = None
self.right = None
self.memory *= 2
self.face = face
def pair (self, parts):
for (ax, ay, aw, ah) in parts:
for (bx, by, bw, bh) in parts:
if ax is bx and ay is by:
continue # same
if abs(ay - by) < ((ah + bh) / 4):
if ax + aw < bx or bx + bw < ax:
return ((ax, ay, aw, ah), (bx, by, bw, bh))
return None
def find (self, frame):
eyes = None
parts = self.cascade.detectMultiScale(frame, 1.2, 2, 0, self.size)
110
h = self.face.last[3] / 2
parts = self.pair(Part.best(self, [
part for part in parts if part[1] + part[3] / 2 < h
]))
if parts is None:
return self.getLast()
return self.setLast(parts)
def setLast (self, parts):
parts = sorted(parts, key=lambda part: part[0])
l1 = parts[0]
r1 = parts[1]
if not self.left is None and not self.right is None:
l0 = self.left
r0 = self.right
111
self.left = [(l0[0] + l1[0]) / 2, (l0[1] + l1[1]) / 2, (l0[2] + l1[2]) / 2, (l0[3] + l1[3]) / 2]
self.right = [(r0[0] + r1[0]) / 2, (r0[1] + r1[1]) / 2, (r0[2] + r1[2]) / 2, (r0[3] + r1[3]) / 2]
else:
self.left = parts[0]
self.right = parts[1]
self.last = [
((self.left[0] + self.right[0]) + (self.left[2] + self.right[2]) / 2) / 2,
((self.left[1] + self.right[1]) + (self.left[3] + self.right[3]) / 2) / 2
]
self.remains = self.memory
return self.last
def highlight (self, frame, color=255):
if self.remains:
x = self.face.last[0]
y = self.face.last[1]
for (ex, ey, ew, eh) in [self.left, self.right]:
112
nx = x + ex + ew / 2
ny = y + ey + eh / 2
cv2.rectangle(frame, (nx - 1, ny - 1), (nx + 1, ny + 1), color)
nx = x + self.last[0]
ny = y + self.last[1]
cv2.rectangle(frame, (nx - 1, ny - 1), (nx + 1, ny + 1), color)