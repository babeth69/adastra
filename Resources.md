1. Large Memory Storage and Retrieval (LAMSTAR) neural networks:

Large Memory Storage and Retrieval (LAMSTAR) neural networks are SOM-based neural networks that can employ a very large number of SOM layers of differing number of neurons to store the LAMSTAR's input sub-words of an input word. The LAMSTAR also employs one or more output (decision) SOM layers and a multitude of link-weights connecting the winning neurons of the various input layers to the decision layers. The link-weights from all winning input layers to a given output lay SOM module are summed. The winning decision is that with the highest link-weight total. Initial weights are all set to zero. The network is fully transparent since the link-weights give the relative significance of a given input layer (sub-word) or even of any input neuron, relative to a given decision. The Lamstar has a forgetting capability, subject to a pre-assigned forgetting factor. Weights are continuously learnt (updated) by punishments and rewards that are added/subtracted to/from each link weight of any winning input neuron after each run or iteration of the input data. The network was developed by Daniel Graupe, Hubert Kordylewsky and Nathan Schneider. See: D Graupe, Principles of Artificial Neural Networks, 2nd edition, World Scientific Publishers, 2007. The LAMSTAR has been used in many applications involving large and diverse data sets ranging from medical diagnosis, economics, finance, cyber security and beyond.