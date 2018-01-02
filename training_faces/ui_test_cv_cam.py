# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'test_cv_cam.ui'
#
# Created by: PyQt5 UI code generator 5.9
#
# WARNING! All changes made in this file will be lost!

from PyQt5 import QtCore, QtGui, QtWidgets

class Ui_CamForm(object):
    def setupUi(self, CamForm):
        CamForm.setObjectName("CamForm")
        CamForm.resize(920, 618)
        CamForm.setStyleSheet("")
        self.centralwidget = QtWidgets.QWidget(CamForm)
        self.centralwidget.setObjectName("centralwidget")
        self.groupBox = QtWidgets.QGroupBox(self.centralwidget)
        self.groupBox.setGeometry(QtCore.QRect(10, 80, 901, 491))
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Preferred, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.groupBox.sizePolicy().hasHeightForWidth())
        self.groupBox.setSizePolicy(sizePolicy)
        self.groupBox.setObjectName("groupBox")
        self.ImgWidget = QtWidgets.QWidget(self.groupBox)
        self.ImgWidget.setGeometry(QtCore.QRect(10, 50, 881, 421))
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Fixed, QtWidgets.QSizePolicy.Fixed)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.ImgWidget.sizePolicy().hasHeightForWidth())
        self.ImgWidget.setSizePolicy(sizePolicy)
        self.ImgWidget.setObjectName("ImgWidget")
        self.coords = QtWidgets.QLabel(self.groupBox)
        self.coords.setGeometry(QtCore.QRect(20, 30, 181, 16))
        self.coords.setObjectName("coords")
        self.startButton = QtWidgets.QPushButton(self.centralwidget)
        self.startButton.setGeometry(QtCore.QRect(10, 10, 901, 61))
        self.startButton.setObjectName("startButton")
        CamForm.setCentralWidget(self.centralwidget)
        self.menubar = QtWidgets.QMenuBar(CamForm)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 920, 21))
        self.menubar.setObjectName("menubar")
        CamForm.setMenuBar(self.menubar)
        self.statusbar = QtWidgets.QStatusBar(CamForm)
        self.statusbar.setObjectName("statusbar")
        CamForm.setStatusBar(self.statusbar)

        self.retranslateUi(CamForm)
        QtCore.QMetaObject.connectSlotsByName(CamForm)

    def retranslateUi(self, CamForm):
        _translate = QtCore.QCoreApplication.translate
        CamForm.setWindowTitle(_translate("CamForm", "MainWindow"))
        self.groupBox.setTitle(_translate("CamForm", "Видео"))
        self.coords.setText(_translate("CamForm", "TextLabel"))
        self.startButton.setText(_translate("CamForm", "Запустить трекер"))

