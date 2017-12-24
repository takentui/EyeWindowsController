# переработка фотки лица в набор координат
import sys
import os
import cv2
import numpy as np
from matplotlib import pyplot as plt
BLOCKS_COUNT = 4
"""
Объявляем параметры которые нужны для обучения
    face_params = (x, y, w, h) координаты и размеры найденного лица
    eyes_params = (x, y, w, h) координаты и размеры пары глаз
    left_eye = (x, y, r) координаты и радиус найденного зрачка
    right_eye = (x, y, r) координаты и радиус найденного зрачка
"""
def decode(block_index):

    face_params = None
    eyes_params = None
    left_eye = None
    right_eye = None

    face_cascade = cv2.CascadeClassifier('haarcascade_frontalface_alt3.xml')
    eye_cascade = cv2.CascadeClassifier('haarcascade_mcs_eyepair_big.xml')
    block_data = []
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
            faces = face_cascade.detectMultiScale(gray, 1.35, 5)
            if not faces is None and len(faces) > 0:
                for (x, y, w, h) in faces:
                    face_params = (x, y, w, h)
                    roi_gray = gray[y:y + h, x:x + w]
                    roi_color = img[y:y + h, x:x + w]
                    roi_gray = cv2.equalizeHist(roi_gray, roi_color)
                    eyes = eye_cascade.detectMultiScale(roi_gray, 1.3, 3)
                    if not eyes is None and len(eyes) > 0:
                        # найдена пара глаз.
                        for (ex, ey, ew, eh) in eyes:
                            eyes_params = (ex, ey, ew, eh)
                            tmp_imp = roi_color[ey:ey + eh, ex:ex + ew]
                            tmp_h, tmp_w, tmp_bpc = tmp_imp.shape
                            # левый глаз
                            half_y = ey + int(eh/2)
                            half_x = ex + int(ew/2)
                            roi_gray_left_img = roi_gray[ey: ey + eh, ex: half_x]
                            # правый глаз
                            roi_gray_right_img = roi_gray[ey: ey + eh, half_x:]
                            # поищем зрачки
                            left_eye = get_circles(roi_gray_left_img, tmp_w)
                            right_eye = get_circles(roi_gray_right_img, tmp_w)
                            left_eye_corners = get_eye_corners(roi_gray_left_img)
                            right_eye_corners = get_eye_corners(roi_gray_right_img)
                            break
                    else:
                        # не найдено. удалить бы фотку
                        pass
                    break
            else:
                pass
                # Лицо на фото не найдено, удалить бы фотку
        face_decoded = {
            'face_params': face_params,
            'eyes_params': eyes_params,
            'left_eye': left_eye,
            'right_eye': right_eye,
            'left_eye_corners': left_eye_corners,
            'right_eye_corners': right_eye_corners,
            'block_id': block_index
        }
        if check_data(face_decoded):
            block_data.append(face_decoded)
    return block_data

def get_circles(img, tmp_w):
    """ Возвращает координаты и радиус для зрачка (x, y ,r)"""

    #img = cv2.medianBlur(img, 5)
    #print(tmp_w)
    circles = cv2.HoughCircles(img,cv2.HOUGH_GRADIENT,1,2, tmp_w/3,
                            param1=30,param2=30,minRadius=0,maxRadius=0)
    if not circles is None:
        circles = np.uint16(np.around(circles))
        for i in circles[0, :]:
            return (i[0], i[1], i[2])
    else:
        return None

def get_eye_corners(img):
    """ Возвращает точки углов на глазах"""

    #blur = cv2.Canny(img, 100, 200)
    img = np.array(img)
    #gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    # blur = cv2.medianBlur(gray,5)
    #blur = cv2.GaussianBlur(gray, (5, 5), 0)
    # blur = cv2.bilateralFilter(gray,9,75,75)
    # img = cv2.threshold(blur,0,255,cv2.THRESH_BINARY+cv2.THRESH_OTSU)
    #img = cv2.adaptiveThreshold(blur, 255, cv2.ADAPTIVE_THRESH_GAUSSIAN_C, cv2.THRESH_BINARY, 11, 2)
    corners = cv2.goodFeaturesToTrack(img, 20, 0.01, 30)
    if not corners is None:
        corners = np.int0(corners)
        # corners = []
        for i in corners:
            # if i>0.01 * max_val:
            x, y = i.ravel()

    return (x, y)


def check_data(data_tpl):
    return not None in data_tpl

if __name__ == '__main__':

    data = []
    for i in range(BLOCKS_COUNT):
        decoded = decode(i)
        data.append(decode(i))

    print(data)