# -*- coding: utf-8 -*-
import PyQt5.QtCore as QtCore
import PyQt5.QtWidgets as QtWidgets
import PyQt5.QtGui as QtGui
import sys
import cv2
import numpy as np
import threading
import time
import queue
import use_part_class as Detector
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

        self.timer = QtCore.QTimer(self)
        self.timer.timeout.connect(self.update_frame)
        self.timer.start(1)

    def start_clicked(self):
        global running
        running = True
        capture_thread.start()
        self.startButton.setEnabled(False)
        self.startButton.setText('Запуск...')

    def update_frame(self):
        if not q.empty():
            self.startButton.setText('Камера запущена')
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
            # img = cv2.resize(img, None, fx=scale, fy=scale, interpolation=cv2.INTER_CUBIC)
            img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
            height, width, bpc = img.shape
            bpl = 3 * width

            ((x, y), screen) = Detector.detect(img)
            # image = QtGui.QPixmap.fromImage(screen)
            # image = QtGui.QImage(screen.data, width, height, bpl, QtGui.QImage.Format_RGB888)
            # self.ImgWidget.setImage(image)
            self.coords.setText("x = {0}, y = {1}".format(x, y))
            cv2.imshow('Фиксация лица', screen)
            # self.ImgWidget.setPixmap(image)

    def closeEvent(self, event):
        global running
        running = False


capture_thread = threading.Thread(target=grab, args=(0, q, 1920, 1080, 20))

app = QtWidgets.QApplication(sys.argv)
w = MyWindowClass(None)
w.setWindowTitle('Контроллер взгляда')
w.show()
app.exec_()