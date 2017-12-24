import random
import cv2
import os
from PyQt5.QtCore import QByteArray, qFuzzyCompare, Qt, QTimer
from PyQt5.QtGui import QPalette, QPixmap
from PyQt5.QtMultimedia import (QAudioEncoderSettings, QCamera,
        QCameraImageCapture, QImageEncoderSettings, QMediaMetaData,
        QMediaRecorder, QMultimedia, QVideoEncoderSettings)
from PyQt5.QtWidgets import (QAction, QActionGroup, QApplication, QDialog,
        QMainWindow, QMessageBox)

from ui_training_form import Ui_MainWindow as UI_TrainingForm
BLOCKS_COUNT = 4
class TrainingForm(QDialog):
    # Основной класс для сохранения фото
    def __init__(self, parent=None):
        super(TrainingForm, self).__init__(parent)

        self.ui = UI_TrainingForm()
        self.ui.setupUi(self)
        self.blocks = [self.ui.block_1, self.ui.block_2, self.ui.block_3, self.ui.block_4]
        self.timer = QTimer(self)

        for i in range(len(self.blocks)):
            if not os.path.exists('{0}'.format(i+1)):
                os.makedirs('{0}'.format(i+1))
        # тут храним кол=во картинок в папке
        self.block_img_counter = []
        for i in range(BLOCKS_COUNT):
            block_index = i + 1
            block_dir = os.walk('{0}'.format(block_index))
            for d, dirs, files in block_dir:
                self.block_img_counter.append(len(files))
        self.timer.timeout.connect(self.timer_tick)
        self.timer.start(1500)
        # cameraDevice = QByteArray()
        self.cam = cv2.VideoCapture(0)
        self.img_number = 1



    def setCentralWidget(self, centralWidget):
        pass

    def setStatusBar(self, statusBar):
        pass

    def timer_tick(self):
        self.point_number = random.randrange(0, len(self.blocks))
        for block in self.blocks:
            block.setStyleSheet("border: 1px solid rgb(0, 0, 0); background:rgb(244, 254, 255)")

        block = self.blocks[self.point_number]
        block.setStyleSheet("border: 1px solid rgb(0, 0, 0); background: #aaffd8;")
        timeout(self.save_img, timeout_duration=300)

    def save_img(self):
        #Сохранение фотки
        _, frame = self.cam.read()
        self.block_img_counter[self.point_number]
        cv2.imwrite('{0}\img_{1}.jpg'.format(self.point_number+1,
                                             self.block_img_counter[self.point_number]), frame)
        print('saved')
        self.block_img_counter[self.point_number] += 1

def timeout(func, args=(), kwargs={}, timeout_duration=1, default=None):
    import threading
    class InterruptableThread(threading.Thread):
        def __init__(self):
            threading.Thread.__init__(self)
            self.result = None

        def run(self):
            try:
                self.result = func(*args, **kwargs)
            except:
                self.result = default

    it = InterruptableThread()
    it.start()
    it.join(timeout_duration)
    if it.isAlive():
        return default
    else:
        return it.result

if __name__ == '__main__':

    import sys

    app = QApplication(sys.argv)

    form = TrainingForm()
    form.show()

    sys.exit(app.exec_())