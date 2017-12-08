# -*- coding: utf-8 -*-

__author__ = "Saulius Lukse"
__copyright__ = "Copyright 2016, kurokesu.com"
__version__ = "0.1"
__license__ = "GPL"

import PyQt5.QtCore as QtCore
import PyQt5.QtWidgets as QtWidgets
import PyQt5.QtGui as QtGui
import sys
import cv2
import numpy as np
import threading
import time
import queue
from ui_test_cv_cam import Ui_CamForm

running = False
capture_thread = None
q = queue.Queue()
face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_alt3.xml')
eye_cascade = cv2.CascadeClassifier('haarcascade_mcs_eyepair_big.xml')


def grab(cam, queue, width, height, fps):
    global running
    capture = cv2.VideoCapture(cam)
    capture.set(cv2.CAP_PROP_FRAME_WIDTH, width)
    capture.set(cv2.CAP_PROP_FRAME_HEIGHT, height)
    capture.set(cv2.CAP_PROP_FPS, fps)

    while (running):
        frame = {}
        capture.grab()
        retval, img = capture.retrieve(0)
        frame["img"] = img

        if queue.qsize() < 10:
            queue.put(frame)
        else:
            print
            queue.qsize()


class OwnImageWidget(QtWidgets.QWidget):
    def __init__(self, parent=None):
        super(OwnImageWidget, self).__init__(parent)
        self.image = None

    def setImage(self, image):
        self.image = image
        sz = image.size()
        self.setMinimumSize(sz)
        self.update()

    def paintEvent(self, event):
        qp = QtGui.QPainter()
        qp.begin(self)
        if self.image:
            qp.drawImage(QtCore.QPoint(0, 0), self.image)
        qp.end()


class MyWindowClass(QtWidgets.QMainWindow, Ui_CamForm):
    def __init__(self, parent=None):
        QtWidgets.QMainWindow.__init__(self, parent)
        self.setupUi(self)

        self.startButton.clicked.connect(self.start_clicked)

        self.window_width = self.ImgWidget.frameSize().width()
        self.window_height = self.ImgWidget.frameSize().height()
        self.ImgWidget = OwnImageWidget(self.ImgWidget)

        self.window_width = self.ImgWidget_2.frameSize().width()
        self.window_height = self.ImgWidget_2.frameSize().height()
        self.ImgWidget_2 = OwnImageWidget(self.ImgWidget_2)

        self.timer = QtCore.QTimer(self)
        self.timer.timeout.connect(self.update_frame)
        self.timer.start(1)

    def start_clicked(self):
        global running
        running = True
        capture_thread.start()
        self.startButton.setEnabled(False)
        self.startButton.setText('Starting...')

    def update_frame(self):
        if not q.empty():
            self.startButton.setText('Camera is live')
            frame = q.get()
            img = frame["img"]
            roi_gray_img = None

            img_height, img_width, img_colors = img.shape
            scale_w = float(self.window_width) / float(img_width)
            scale_h = float(self.window_height) / float(img_height)
            scale = min([scale_w, scale_h])

            if scale == 0:
                scale = 1

            scale = 1
            img = cv2.resize(img, None, fx=scale, fy=scale, interpolation=cv2.INTER_CUBIC)
            img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
            height, width, bpc = img.shape
            bpl = bpc * width
            tmp_imp = None
            gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
            # gray_hist = cv2.equalizeHist(gray, img)
            faces = face_cascade.detectMultiScale(gray, 1.35, 5)
            for (x, y, w, h) in faces:
                cv2.rectangle(img, (x, y), (x + w, y + h), (255, 0, 0), 2)
                roi_gray = gray[y:y + h, x:x + w]
                roi_color = img[y:y + h, x:x + w]
                roi_gray = cv2.equalizeHist(roi_gray, roi_color)
                eyes = eye_cascade.detectMultiScale(roi_gray, 1.3, 3)
                for (ex, ey, ew, eh) in eyes:
                    cv2.rectangle(roi_color, (ex, ey), (ex + ew, ey + eh), (0, 255, 0), 2)
                    tmp_imp = roi_color[ey:ey+eh, ex:ex+ew]
                    roi_gray_img = roi_gray[ey:ey+eh, ex:ex+ew]
                    # roi_gray_img = cv2.blur(roi_gray_img, ksize=(3, 3))

                    break
                break

            image = QtGui.QImage(img.data, width, height, bpl, QtGui.QImage.Format_RGB888)
            if not tmp_imp is None:
                tmp_h, tmp_w, tmp_bpc = tmp_imp.shape
                tmp_bpl = tmp_bpc * tmp_w
                circles = cv2.HoughCircles(roi_gray_img, cv2.HOUGH_GRADIENT, 1, tmp_w / 3,
                                           circles=2, param1=30, param2=30, minRadius=0, maxRadius=0)
                if not circles is None:
                    circles = np.uint16(np.around(circles))
                    for i in circles[0, :]:
                        # draw the outer circle
                        cv2.circle(tmp_imp, (i[0], i[1]), i[2], (0, 255, 0), 2)
                        # draw the center of the circle
                        cv2.circle(tmp_imp, (i[0], i[1]), 2, (0, 0, 255), 1)

                image2 = QtGui.QImage(bytes(tmp_imp.data), tmp_w, tmp_h, tmp_bpl, QtGui.QImage.Format_RGB888)

                self.ImgWidget.setImage(image2)

            self.ImgWidget_2.setImage(image)

    def closeEvent(self, event):
        global running
        running = False


capture_thread = threading.Thread(target=grab, args=(0, q, 800, 600, 20))

app = QtWidgets.QApplication(sys.argv)
w = MyWindowClass(None)
w.setWindowTitle('Kurokesu PyQT OpenCV USB camera test panel')
w.show()
app.exec_()