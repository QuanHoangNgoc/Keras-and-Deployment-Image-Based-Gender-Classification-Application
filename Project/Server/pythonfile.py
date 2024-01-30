"""
import keras 
import numpy as np
from keras.preprocessing import image
from datetime import datetime 

image_path = "D:\\cd_data_C\\Desktop\\App\\a.jpg" 
INPUT_SIZE = (64, 64) 

# image input: load and preprocessing 
print(">>> image input:")
print(image_path)
imge = image.load_img(image_path, target_size=INPUT_SIZE) 
x = image.img_to_array(imge)
print(x.shape) 
x = np.expand_dims(x, axis=0) / 255 
print(x.shape, type(x)) 
x = np.vstack([x])
print(x.shape, type(x)) 

# load model 
nameModel = open("D:\cd_data_C\Desktop\App\config.txt").readline()
model = keras.saving.load_model(nameModel)
print(">>> load model 100%.")

# predict 
print(">>> predict:")
classes = model.predict(x, batch_size=1)
print(classes) 
# result 
if classes[0]>0.5: res = "This is a male"
else: res = "This is a female"
if(classes[0] > 0.3 and classes[0] < 0.7): res += ". But I am not sure."
print(res) 

# now 
print(">>> now:", datetime.now()) 
"""

res = "I am one."