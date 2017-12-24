import numpy as np

npzfile = np.load(file)
train_eye_left = npzfile["train_eye_left"]
train_eye_right = npzfile["train_eye_right"]
train_face = npzfile["train_face"]
train_face_mask = npzfile["train_face_mask"]
train_y = npzfile["train_y"]
val_eye_left = npzfile["val_eye_left"]
val_eye_right = npzfile["val_eye_right"]
val_face = npzfile["val_face"]
val_face_mask = npzfile["val_face_mask"]
val_y = npzfile["val_y"]