import random
from PyQt5.QtCore import QByteArray, qFuzzyCompare, Qt, QTimer
from PyQt5.QtGui import QPalette, QPixmap
from PyQt5.QtMultimedia import (QAudioEncoderSettings, QCamera,
        QCameraImageCapture, QImageEncoderSettings, QMediaMetaData,
        QMediaRecorder, QMultimedia, QVideoEncoderSettings)
from PyQt5.QtWidgets import (QAction, QActionGroup, QApplication, QDialog,
        QMainWindow, QMessageBox)

from ui_training_form import Ui_MainWindow as UI_TrainingForm

class TrainingForm(QDialog):
    # Основной класс для сохранения фото
    def __init__(self, parent=None):
        super(TrainingForm, self).__init__(parent)

        self.ui = UI_TrainingForm()
        self.ui.setupUi(self)
        self.blocks = [self.ui.block_1, self.ui.block_2, self.ui.block_3, self.ui.block_4]
        self.timer = QTimer(self)
        self.timer.timeout.connect(self.timer_tick)
        self.timer.start(1500)

    def setCentralWidget(self, centralWidget):
        pass

    def setStatusBar(self, statusBar):
        pass

    def timer_tick(self):
        rand = random.randrange(0, len(self.blocks))
        for block in self.blocks:
            block.setStyleSheet("border: 1px solid rgb(0, 0, 0); background:rgb(244, 254, 255)")

        block = self.blocks[rand]
        block.setStyleSheet("border: 1px solid rgb(0, 0, 0); background: #aaffd8;");

if __name__ == '__main__':

    import sys

    app = QApplication(sys.argv)

    form = TrainingForm()
    form.show()

    sys.exit(app.exec_())