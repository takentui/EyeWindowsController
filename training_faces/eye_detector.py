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

from ui_training_form import Ui_MainWindow as UI_TrainingForm

BLOCKS_COUNT = 4

running = False
capture_thread = None
q = queue.Queue()



class TrainingForm(QtWidgets.QDialog):
    # Основной класс для сохранения фото
    def __init__(self, parent=None):
        super(TrainingForm, self).__init__(parent)

        self.ui = UI_TrainingForm()
        self.ui.setupUi(self)
        # self.blocks = [self.ui.block_1, self.ui.block_2, self.ui.block_3, self.ui.block_4]
        # self.timer = QtWidgets.QTimer(self)
        #
        # self.timer.timeout.connect(self.timer_tick)
        # self.timer.start(1500)
        # cameraDevice = QByteArray()
        # self.cam = cv2.VideoCapture(0)
        self.img_number = 1

    def print_block(self, number):
        """ Подсветка блока"""
        block = self.ui["block_{0}".format(number)]
        block.setStyleSheet("border: 1px solid rgb(0, 0, 0); background: #aaffd8;")


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
        self.window_width = 800
        self.window_height = 600

        # Подписка на настройки
        # Центровка рта
        self.mvcc = 0.3
        # Главный коэффициент поворота головы
        self.k = 0.5
        # X коррекция
        self.mxc = 0.05
        # Y коррекция
        self.myc = 0.035
        self.mvcc_slider.valueChanged.connect(self.update_mvcc)
        self.mxc_slider.valueChanged.connect(self.update_mxc)
        self.myc_slider.valueChanged.connect(self.update_myc)
        self.mkc_slider.valueChanged.connect(self.update_mkc)


        self.timer = QtCore.QTimer(self)
        self.timer.timeout.connect(self.update_frame)
        self.timer.start(1)

    def start_clicked(self):
        global running
        running = True
        capture_thread.start()
        self.startButton.setEnabled(False)
        self.startButton.setText('Запуск...')

    def update_mvcc(self, value):
        self.mvcc = int(value) / 100

    def update_mxc(self, value):
        self.mxc = int(value) / 1000

    def update_myc(self, value):
        self.myc = int(value) / 1000

    def update_mkc(self, value):
        self.k = int(value) / 100


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

            ((x, y), screen) = Detector.detect(img, self.mvcc, self.k, self.mxc, self.myc)
            # image = QtGui.QPixmap.fromImage(screen)
            # image = QtGui.QImage(screen.data, width, height, bpl, QtGui.QImage.Format_RGB888)
            # self.ImgWidget.setImage(image)
            self.settings.setText("mxc = {0}, myc = {1}, mvcc = {2}, k = {3}".format(self.mxc, self.myc, self.mvcc, self.k))
            self.coords.setText("x = {0}, y = {1}".format(x, y))
            cv2.imshow('Фиксация лица', screen)
            self.show_grid(x, y)
            # self.ImgWidget.setPixmap(image)

    def show_grid(self, x, y):
        """ Отображение сетки"""
        w, h = 640, 480
        grid = np.zeros((h, w, 1), np.uint8);
        w3 = int(w / 3)
        h3 = int(h / 3)
        cv2.rectangle(grid, (0, h3), (w - 1, 2 * h3), 255)
        cv2.rectangle(grid, (w3, 0), (2 * w3, h - 1), 255)
        nx = int(x / w3)
        ny = int(y / h3)

        sys.stdout.flush()
        # if silent:
        #     continue
        # Highlight selected
        cv2.rectangle(grid, (nx * w3, ny * h3), ((nx + 1) * w3, (ny + 1) * h3), 127, cv2.FILLED)
        # Draw cursor
        cv2.rectangle(grid, (x - 4, y - 4), (x + 4, y + 4), 0)
        cv2.rectangle(grid, (x - 2, y - 2), (x + 2, y + 2), 0, cv2.FILLED)
        # Highlight selected
        cv2.rectangle(grid, (nx * w3, ny * h3), ((nx + 1) * w3, (ny + 1) * h3), 127, cv2.FILLED)
        # Draw cursor
        cv2.rectangle(grid, (x - 4, y - 4), (x + 4, y + 4), 0)
        cv2.rectangle(grid, (x - 2, y - 2), (x + 2, y + 2), 0, cv2.FILLED)
        cv2.imshow('grid', grid)

    def closeEvent(self, event):
        global running
        running = False


capture_thread = threading.Thread(target=grab, args=(0, q, 1920, 1080, 20))
app = QtWidgets.QApplication(sys.argv)
w = MyWindowClass(None)
w.setWindowTitle('Контроллер взгляда')
w.show()

face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_alt3.xml')
eye_cascade = cv2.CascadeClassifier('haarcascade_mcs_eyepair_big.xml')
# form = TrainingForm()
# form.show()
app.exec_()
