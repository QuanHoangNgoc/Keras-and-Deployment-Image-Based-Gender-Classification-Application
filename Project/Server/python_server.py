from flask import Flask, request 
from flask_cors import CORS, cross_origin
import tensorflow as tf 
import keras 

############################## CONST AND DIVICE CONFIG ##############################
MODEL_PATH = 'D:\cd_data_C\Desktop\App\model.h5'
MODEL_PATH_2 = 'D:\cd_data_C\Desktop\App\model321.h5'
HOST = '0.0.0.0'
PORT = '6789' 
client_request_key = ("imageB64")
image_path = 'D:\cd_data_C\Desktop\App\some_image.jpg'
INPUT_SIZE = (64, 64) 
ratio_for_big = 0.5 

############################## create graph for model ##############################
sess = tf.compat.v1.Session()
graph_model = tf.compat.v1.get_default_graph()
### load model safety 
with sess.as_default(): 
    with graph_model.as_default(): 
        gender_model = keras.saving.load_model(MODEL_PATH)
        model = keras.saving.load_model(MODEL_PATH_2)

############################## create server ##############################
app = Flask(__name__)
CORS(app=app)
app.config['CORS_HEADERS'] = 'Content-Type'

############################## function ##############################
import base64
from io import BytesIO
from PIL import Image
import numpy as np
def base64_to_numpy(base64_string):
    # Decode Base64 to bytes
    image_data = base64.b64decode(base64_string)
    # Create a binary stream
    image_stream = BytesIO(image_data)
    # Open the image using PIL
    pil_image = Image.open(image_stream)
    # Convert PIL image to NumPy array
    numpy_array = np.array(pil_image)
    return numpy_array

from keras.preprocessing import image
def base64_to_image(imgstring): 
    imgdata = base64.b64decode(imgstring)
    filename = image_path 
    # I assume you have a way of picking unique filenames
    with open(filename, 'wb') as f:
        f.write(imgdata)
# f gets closed when you exit the with statement
# Now save the value of filename to your database

############################## api for request from client ##############################
@app.route('/model_predict', methods=['POST', 'GET'])
@cross_origin(origin='*')
def model_predict_process() -> str: 
    # get mes from client_request_key 
    ib64 = request.form.get("imageB64")
    
    # convert to svae image  
    base64_to_image(ib64) 
    imge = image.load_img(image_path, target_size=INPUT_SIZE) 
    # preprocessing to numpy 
    x = image.img_to_array(imge)
    print(x.shape) 
    x = np.expand_dims(x, axis=0) / 255 
    print(x.shape, type(x)) 
    x = np.vstack([x])
    print(x.shape, type(x)) 
    
    # predict and return string 
    with sess.as_default(): 
        with graph_model.as_default(): 
            pro1, pro2 = round(gender_model.predict(x, batch_size=1)[0][0] * 100), round(model.predict(x, batch_size=1)[0][0] * 100) 
            classes = gender_model.predict(x, batch_size=1) * ratio_for_big + model.predict(x, batch_size=1) * (1-ratio_for_big) 
            
            if classes[0]>0.5: respond = "This is a male: ({0}; {1})male and ({2}; {3})female".format(pro1, pro2, 100-pro1, 100-pro2)
            else: respond = "This is a female: ({0}; {1})male and ({2}; {3})female".format(pro1, pro2, 100-pro1, 100-pro2)
            
            print(respond) 
            return respond

@app.route('/')
@cross_origin()
def index():
    return "Welcome to flask API!"
@app.route('/hello')
@cross_origin()
def hello():
    return "Hello!"

############################## main, host and port ##############################
### dubug MUST True to run not error  
if __name__ == '__main__':
    app.run(debug=True, host=HOST, port=PORT)