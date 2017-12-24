import cv2
import numpy as np
import os
from matplotlib import pyplot as plt

nose_cascade = cv2.CascadeClassifier('haarcascade_mcs_nose.xml')
mouth_cascade = cv2.CascadeClassifier('haarcascade_mcs_mouth.xml')

block_data = []
block_index = 1
block_dir = os.walk('{0}'.format(block_index))
for d, dirs, files in block_dir:
    # Пройдем по всем папкам
    for f in files:
        img = cv2.imread('{0}/{1}'.format(block_index, f))
        gray = cv2.cvtColor(img,cv2.COLOR_BGR2GRAY)

        height, width, bpc = img.shape
        bpl = bpc * width
        tmp_imp = None
        gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        # gray_hist = cv2.equalizeHist(gray, img)
        faces = nose_cascade.detectMultiScale(gray, 1.35, 5)
        if not faces is None and len(faces) > 0:
            for (x, y, w, h) in faces:
                face_params = (x, y, w, h)
                roi_gray = gray[y:y + h, x:x + w]
                roi_color = img[y:y + h, x:x + w]
                cv2.rectangle(img, (x, y), (x + w, y + h), (255, 0, 0), 2)
                roi_gray = cv2.equalizeHist(roi_gray, roi_color)
                eyes = mouth_cascade.detectMultiScale(roi_gray, 1.35, 5)
                print(len(eyes))
                if not eyes is None and len(eyes) > 0:
                    # найдена пара глаз.
                    for (ex, ey, ew, eh) in eyes:
                        eyes_params = (ex, ey, ew, eh)
                        tmp_imp = roi_color[ey:ey + eh, ex:ex + ew]
                        cv2.rectangle(img, (ex, ey), (ex + ew, ey + eh), (0, 255, 0), 2)
                        tmp_h, tmp_w, tmp_bpc = tmp_imp.shape
                        # # левый глаз
                        # half_y = ey + int(eh/2)
                        # half_x = ex + int(ew/2)
                        # roi_gray_left_img = roi_gray[ey: ey + eh, ex: half_x]
                        # # правый глаз
                        # roi_gray_right_img = roi_gray[ey: ey + eh, half_x:]
                        # # поищем зрачки
                        # left_eye = get_circles(roi_gray_left_img, tmp_w)
                        # right_eye = get_circles(roi_gray_right_img, tmp_w)
                        # left_eye_corners = get_eye_corners(roi_gray_left_img)
                        # right_eye_corners = get_eye_corners(roi_gray_right_img)
                        break
                else:
                    # не найдено. удалить бы фотку
                    pass
                break
            plt.imshow(img)
            plt.show()
        else:
            pass