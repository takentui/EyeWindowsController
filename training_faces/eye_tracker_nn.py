import numpy as np
import tensorflow as tf
import matplotlib.pyplot as plt

# Закинем тестовые данные


samples = 50 # количество точек
packetSize = 5 # размер пакета
def f(x, z): return 2*x-z+3 # искомая функция
x_0 = -2 # начало интервала
x_l = 2 # конец интервала
sigma = 0.5 # среднеквадратическое отклонение шума

# np.random.seed(0) # делаем случайность предсказуемой (чтобы все желающие могли повторить вычисления на этом же наборе данных)
data_x = np.arange(x_0,x_l,(x_l-x_0)/samples) # массив [-2, -1.92, -1.84, ..., 1.92, 2]
data_z = np.arange(0, 5, 5/samples)
np.random.shuffle(data_x) # перемешать, но не взбалтывать
data_y = list(map(f, data_x, data_z)) + np.random.normal(0, sigma, samples) # массив значений функции с шумом

# kek

# Parameters
learning_rate = 0.001
training_iters = 10
batch_size = 128
display_step = 10

# Network Parameters
n_input = 784 # MNIST data input (img shape: 28*28)
n_classes = 10 # MNIST total classes (0-9 digits)
dropout = 0.75 # Dropout, probability to keep units

# tf Graph input
x = tf.placeholder(tf.float32, [None, n_input])
y = tf.placeholder(tf.float32, [None, n_classes])
keep_prob = tf.placeholder(tf.float32) #dropout (keep probability)

# Create some wrappers for simplicity
def _conv2d(x, W, b, strides=None):
    # Conv2D wrapper, with bias and relu activation
    x = tf.nn.conv2d(x, W, strides=[1, strides, strides, 1], padding='SAME')
    # x = tf.nn.bias_add(x, b)
    return tf.nn.relu(x)


def maxpool2d(x, k=2):
    # MaxPool2D wrapper
    return tf.nn.max_pool(x, ksize=[1, k, k, 1], strides=[1, k, k, 1],
                          padding='SAME')

def saliency_path(x,weights,biases):
    conv1 = _conv2d(x, weights['wc1'], biases['bc1'])


# Create model
def conv_net(x, weights, biases, dropout):
    # Reshape input picture
    # x = tf.reshape(x, shape=[-1, 28, 28, 1])

    # Convolution Layer
    conv1 = _conv2d(x, weights['wc1'], biases['bc1'], 1)
    # Max Pooling (down-sampling)
    conv1 = maxpool2d(conv1, k=2)

    # Convolution Layer
    conv2 = _conv2d(conv1, weights['wc2'], biases['bc2'])
    # Max Pooling (down-sampling)
    conv2 = maxpool2d(conv2, k=2)

    # Fully connected layer
    # Reshape conv2 output to fit fully connected layer input
    fc1 = tf.reshape(conv2, [-1, weights['wd1'].get_shape().as_list()[0]])
    fc1 = tf.add(tf.matmul(fc1, weights['wd1']), biases['bd1'])
    fc1 = tf.nn.relu(fc1)
    # Apply Dropout
    fc1 = tf.nn.dropout(fc1, dropout)

    # Output, class prediction
    out = tf.add(tf.matmul(fc1, weights['out']), biases['out'])
    return out

# Store layers weight & bias
weights = {
    # 5x5 conv, 1 input, 32 outputs
    'wc1': tf.Variable(tf.random_normal([5, 5, 1, 32])),
    # 5x5 conv, 32 inputs, 64 outputs
    'wc2': tf.Variable(tf.random_normal([5, 5, 32, 64])),
    # fully connected, 7*7*64 inputs, 1024 outputs
    'wd1': tf.Variable(tf.random_normal([7*7*64, 1024])),
    # 1024 inputs, 10 outputs (class prediction)
    'out': tf.Variable(tf.random_normal([1024, n_classes]))
}

biases = {
    'bc1': tf.Variable(tf.random_normal([32])),
    'bc2': tf.Variable(tf.random_normal([64])),
    'bd1': tf.Variable(tf.random_normal([1024])),
    'out': tf.Variable(tf.random_normal([n_classes]))
}

# Construct model
pred = conv_net(x, weights, biases, keep_prob)

# Define loss and optimizer
cost = tf.reduce_mean(tf.nn.softmax_cross_entropy_with_logits(logits=pred, labels=y))
optimizer = tf.train.AdamOptimizer(learning_rate=learning_rate).minimize(cost)

# Evaluate model
correct_pred = tf.equal(tf.argmax(pred, 1), tf.argmax(y, 1))
accuracy = tf.reduce_mean(tf.cast(correct_pred, tf.float32))

# Initializing the variables

# Uncomment this section if I want to use GPU
init = tf.global_variables_initializer()
config = tf.ConfigProto(
    device_count={'GPU': 0}
)

print("Training will start now!")

# Launch the graph
with tf.Session(config=config) as sess:
    summary_writer = tf.summary.FileWriter('log_simple_graph', sess.graph)
    with tf.device("/cpu:0"):
        sess.run(init)
        step = 1
        # Keep training until reach max iterations
        # while step * batch_size < training_iters:
        for i in range(samples // packetSize):
            # batch_x, batch_y = mnist.train.next_batch(batch_size)
            # Run optimization op (backprop)
            sess.run(optimizer,
                    feed_dict={x: data_x[i*packetSize: (i+1)*packetSize],
                               y: data_z[i*packetSize: (i+1)*packetSize],
                               keep_prob: data_y[i*packetSize: (i+1)*packetSize]})
            if i % display_step == 0:
                # Calculate batch loss and accuracy
                loss, acc = sess.run([cost, accuracy],
                                     feed_dict={x: data_x[i*packetSize: (i+1)*packetSize],
                                     y: data_z[i*packetSize: (i+1)*packetSize],
                                     keep_prob: data_y[i*packetSize: (i+1)*packetSize]})
                summary_writer.add_summary(loss, i)
                print("Iter " + str(step * batch_size) + ", Minibatch Loss= " + \
                      "{:.6f}".format(loss) + ", Training Accuracy= " + \
                      "{:.5f}".format(acc))
            step += 1
        print("Optimization Finished!")

        # Calculate accuracy for 256 mnist test images
        print("Testing Accuracy:", \
              sess.run(accuracy, feed_dict={x: data_x,
                                            y: data_z,
                                            keep_prob: data_y}))