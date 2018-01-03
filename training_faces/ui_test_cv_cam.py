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
        CamForm.resize(385, 466)
        CamForm.setStyleSheet("")
        self.centralwidget = QtWidgets.QWidget(CamForm)
        self.centralwidget.setObjectName("centralwidget")
        self.groupBox = QtWidgets.QGroupBox(self.centralwidget)
        self.groupBox.setGeometry(QtCore.QRect(10, 80, 371, 341))
        sizePolicy = QtWidgets.QSizePolicy(QtWidgets.QSizePolicy.Preferred, QtWidgets.QSizePolicy.Preferred)
        sizePolicy.setHorizontalStretch(0)
        sizePolicy.setVerticalStretch(0)
        sizePolicy.setHeightForWidth(self.groupBox.sizePolicy().hasHeightForWidth())
        self.groupBox.setSizePolicy(sizePolicy)
        self.groupBox.setObjectName("groupBox")
        self.coords = QtWidgets.QLabel(self.groupBox)
        self.coords.setGeometry(QtCore.QRect(20, 30, 181, 16))
        self.coords.setObjectName("coords")
        self.mxc_slider = QtWidgets.QSlider(self.groupBox)
        self.mxc_slider.setGeometry(QtCore.QRect(20, 110, 331, 19))
        self.mxc_slider.setOrientation(QtCore.Qt.Horizontal)
        self.mxc_slider.setObjectName("mxc_slider")
        self.label = QtWidgets.QLabel(self.groupBox)
        self.label.setGeometry(QtCore.QRect(20, 90, 101, 16))
        self.label.setObjectName("label")
        self.label_2 = QtWidgets.QLabel(self.groupBox)
        self.label_2.setGeometry(QtCore.QRect(20, 150, 81, 16))
        self.label_2.setObjectName("label_2")
        self.myc_slider = QtWidgets.QSlider(self.groupBox)
        self.myc_slider.setGeometry(QtCore.QRect(20, 170, 331, 19))
        self.myc_slider.setOrientation(QtCore.Qt.Horizontal)
        self.myc_slider.setObjectName("myc_slider")
        self.mkc_slider = QtWidgets.QSlider(self.groupBox)
        self.mkc_slider.setGeometry(QtCore.QRect(20, 230, 331, 19))
        self.mkc_slider.setOrientation(QtCore.Qt.Horizontal)
        self.mkc_slider.setObjectName("mkc_slider")
        self.label_3 = QtWidgets.QLabel(self.groupBox)
        self.label_3.setGeometry(QtCore.QRect(20, 210, 81, 16))
        self.label_3.setObjectName("label_3")
        self.label_4 = QtWidgets.QLabel(self.groupBox)
        self.label_4.setGeometry(QtCore.QRect(20, 270, 101, 16))
        self.label_4.setObjectName("label_4")
        self.mvcc_slider = QtWidgets.QSlider(self.groupBox)
        self.mvcc_slider.setGeometry(QtCore.QRect(20, 300, 331, 19))
        self.mvcc_slider.setOrientation(QtCore.Qt.Horizontal)
        self.mvcc_slider.setObjectName("mvcc_slider")
        self.settings = QtWidgets.QLabel(self.groupBox)
        self.settings.setGeometry(QtCore.QRect(20, 60, 341, 16))
        self.settings.setObjectName("settings")
        self.startButton = QtWidgets.QPushButton(self.centralwidget)
        self.startButton.setGeometry(QtCore.QRect(10, 10, 371, 61))
        self.startButton.setObjectName("startButton")
        CamForm.setCentralWidget(self.centralwidget)
        self.menubar = QtWidgets.QMenuBar(CamForm)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 385, 21))
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
        self.groupBox.setTitle(_translate("CamForm", "Настройки"))
        self.coords.setText(_translate("CamForm", "TextLabel"))
        self.label.setText(_translate("CamForm", "Х - коррекция"))
        self.label_2.setText(_translate("CamForm", "У- коррекция"))
        self.label_3.setText(_translate("CamForm", "K - Коррекция"))
        self.label_4.setText(_translate("CamForm", "Центрование рта"))
        self.settings.setText(_translate("CamForm", "TextLabel"))
        self.startButton.setText(_translate("CamForm", "Запустить трекер"))

