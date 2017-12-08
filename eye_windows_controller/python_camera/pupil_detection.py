# Пример поиска зрачков
# Пример работы метода CannyEdge
import cv2
import numpy as np
from matplotlib import pyplot as plt

img = cv2.imread('pupil.png',0)
cimg = cv2.cvtColor(img,cv2.COLOR_GRAY2BGR)
# img = cv2.medianBlur(img,5)
rows,cols = img.shape
circles = cv2.HoughCircles(img,cv2.HOUGH_GRADIENT,1,2, cols/3,
                            param1=30,param2=30,minRadius=0,maxRadius=0)

circles = np.uint16(np.around(circles))
for i in circles[0,:]:
    # draw the outer circle
    cv2.circle(cimg,(i[0],i[1]),i[2],(0,255,0),2)
    # draw the center of the circle
    cv2.circle(cimg,(i[0],i[1]),2,(0,0,255),1)
plt.subplot(121),plt.imshow(img,cmap = 'gray')
plt.title('Original Image'), plt.xticks([]), plt.yticks([])
plt.subplot(122),plt.imshow(cimg,cmap = 'gray')
plt.title('Edge Image'), plt.xticks([]), plt.yticks([])
plt.show()