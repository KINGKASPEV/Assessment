# Assessment


### Explanation of Design Choices for Section 1: Object-Oriented Programming with C#/.NET Core

#### Inheritance and Polymorphism

1. **Base Class: NetworkDevice**
   - **Properties: IPAddress (string), MACAddress (string)**
     - **Reason**: These properties are common to all network devices. The IPAddress uniquely identifies a device on a network, while the MACAddress is a unique identifier assigned to network interfaces for communications on the physical network segment. Defining these properties in the base class ensures that every network device has these essential attributes.
   
   - **Method: Connect() (abstract method)**
     - **Reason**: By defining `Connect()` as an abstract method, we enforce that every derived class must provide its own implementation. This leverages polymorphism, allowing us to treat all network devices uniformly while ensuring specific behaviors are implemented by each device type.
   
   - **Constructor: Initializes IPAddress and MACAddress**
     - **Reason**: The constructor ensures that every instance of `NetworkDevice` or its derived classes will have its `IPAddress` and `MACAddress` properly initialized. This promotes data integrity and consistency across all network devices.

2. **Derived Classes: Router, Switch, AccessPoint**
   
   - **Router**
     - **Property: NumberOfPorts (int)**
       - **Reason**: This property is specific to routers, which typically have multiple ports for connecting various devices and networks. Including `NumberOfPorts` provides detailed information about the router's capabilities.
     - **Override Connect()**
       - **Reason**: The `Connect()` method is overridden to simulate the behavior specific to a router connecting to the network. This allows for different connection logic that matches the real-world operation of a router.

   - **Switch**
     - **Property: NumberOfPorts (int)**
       - **Reason**: Similar to routers, switches also have multiple ports. The `NumberOfPorts` property in the `Switch` class provides a clear distinction of its capacity to handle network connections.
     - **Override Connect()**
       - **Reason**: Overriding the `Connect()` method allows the switch to implement its specific connection logic, simulating the way switches manage network traffic.

   - **AccessPoint**
     - **Property: SSID (string)**
       - **Reason**: An Access Point (AP) typically has a Service Set Identifier (SSID), which is a unique name that identifies the wireless network. This property is crucial for distinguishing between different APs in a wireless network environment.
     - **Override Connect()**
       - **Reason**: The `Connect()` method is overridden to simulate how an access point connects to a network, which is distinct from how routers and switches operate. This customization reflects the unique role of an AP in providing wireless connectivity.

### Summary

The design leverages the principles of object-oriented programming, specifically inheritance and polymorphism, to create a flexible and extensible model for network devices. By defining common properties and methods in the `NetworkDevice` base class, we ensure consistency and reuse. Overriding the `Connect()` method in derived classes allows each device type to implement its specific connection logic, enhancing realism and functionality in simulations or applications. This approach promotes code clarity, maintainability, and scalability, as new types of network devices can be easily added by extending the base class and providing specific implementations.

### Explanation of Design Choices for SECTION 3: DATABASE SYSTEMS
Relational and Document-Oriented Databases

#### 1. Relational Database (SQL Server)

**Table Design:**

- **Product Table**: 
  - **ProductID (int)**: This is the primary key for the table, which uniquely identifies each product. An integer type is chosen for efficiency.
  - **ProductName (string)**: This column stores the name of the product. A string type is chosen to store text data.
  - **Price (decimal)**: This column stores the price of the product. A decimal type is chosen to handle monetary values accurately without rounding errors.

**Operations:**

- **Insert a new product**: 
  - This method allows adding new products to the `Product` table. It ensures that new entries can be recorded in the database.
- **Retrieve all products**:
  - This method retrieves all products from the `Product` table. It is useful for displaying a list of products or for performing operations on the entire product catalog.

**Choice Rationale:**

- SQL Server is used for structured data that requires ACID (Atomicity, Consistency, Isolation, Durability) properties, and complex queries. The relational model is well-suited for the product data because it involves structured data with relationships (if extended).

#### 2. Document-Oriented Database (MongoDB)

**Collection Design:**

- **Customer Collection**:
  - **CustomerId (ObjectId)**: This is the primary key for the collection, automatically generated by MongoDB to uniquely identify each document.
  - **Name (string)**: This field stores the name of the customer.
  - **Email (string)**: This field stores the email address of the customer.

**Operations:**

- **Insert a new customer**: 
  - This method allows adding new customers to the `Customer` collection. It ensures that new customer entries can be recorded in the database.
- **Retrieve all customers**:
  - This method retrieves all customers from the `Customer` collection. It is useful for displaying a list of customers or for performing operations on the entire customer base.

**Choice Rationale:**

- MongoDB is used for unstructured or semi-structured data where schema flexibility is required. The document-oriented model is well-suited for customer data because it allows for easy scaling and flexibility in handling different customer attributes without predefined schema constraints.

### Implementation Details

**Configuration Management:**

- The `appsettings.json` file is used to store database connection strings for both SQL Server and MongoDB. This approach centralizes configuration and makes it easy to manage and change database connections without modifying the code.

**SQL Server Configuration:**

- `ProductDbContext` class is configured to use SQL Server with Entity Framework Core (EF Core). EF Core provides an abstraction over the database, allowing easy CRUD operations without writing raw SQL queries.

**MongoDB Configuration:**

- `MongoDbContext` class initializes the MongoDB connection and provides access to the `Customer` collection. Using the MongoDB C# driver, the context is configured to connect to the MongoDB instance.

**Service Layer:**

- **ProductService**: 
  - Provides methods to add and retrieve products using `ProductDbContext`.
- **CustomerService**: 
  - Provides methods to add and retrieve customers using `MongoDbContext`.

### Conclusion

This design effectively utilizes the strengths of both relational and document-oriented databases:

- **SQL Server** for structured, relational data that benefits from ACID transactions and complex querying capabilities.
- **MongoDB** for flexible, schema-less data storage that can easily handle changes in data structure and scale horizontally.

By separating concerns into different service classes (`ProductService` and `CustomerService`), the design promotes clean architecture and makes the application easier to maintain and extend.


### Explanation of Design Choices for Section 4: Computer Networks and Distributed Systems

#### Peer-to-Peer (P2P) Network Simulation

1. **Each Peer Running in a Separate Thread**
   - **Reason**: Running each peer in a separate thread allows for concurrent execution, which is essential in a P2P network where multiple peers need to operate independently and simultaneously. This design choice ensures that the application can handle multiple connections and messages without blocking or delaying other operations. Threads enable the application to be responsive and efficient in handling network communications.

2. **Implement Methods to Send and Receive Messages**
   - **Reason**: In a P2P network, peers must be able to communicate directly with each other without relying on a centralized server. Implementing methods for sending and receiving messages is crucial for enabling this direct communication. These methods ensure that peers can exchange information dynamically, facilitating real-time interaction within the network.

### Detailed Breakdown

1. **Class Structure and Methods**

   - **Class: Peer**
     - **Properties**:
       - `_listener`: An instance of `TcpListener` that listens for incoming connections.
       - `_clients`: A list of `TcpClient` objects representing the connected peers.
     - **Constructor**:
       - Initializes the `TcpListener` with a specified port and sets up the necessary properties for handling connections and clients.
   
   - **Method: StartListening()**
     - **Reason**: This method starts the `TcpListener` and continuously listens for incoming connections in a non-blocking manner using a separate task. It adds new clients to the `_clients` list and handles them asynchronously. This design ensures the application can accept and process multiple connections concurrently.
   
   - **Method: ConnectToPeer(string ipAddress, int port)**
     - **Reason**: This method allows a peer to connect to another peer by creating a new `TcpClient` and establishing a connection to the specified IP address and port. This functionality is crucial for forming the network by connecting peers dynamically.

   - **Method: HandleClient(TcpClient client)**
     - **Reason**: This method handles communication with a connected peer. It reads incoming messages and broadcasts them to other connected peers, except the sender. Using asynchronous operations for reading and writing ensures that the application remains responsive and can handle multiple clients efficiently.
   
   - **Method: SendMessage(string message)**
     - **Reason**: This method sends a message to all connected peers. By iterating through the `_clients` list and writing the message to each peer's stream, it ensures that all peers receive the message. This method is fundamental for enabling group communication within the P2P network.
   
   - **Method: BroadcastMessage(string message, TcpClient excludeClient)**
     - **Reason**: This method broadcasts a received message to all connected peers, excluding the sender. It ensures that messages are propagated throughout the network, enabling all peers to stay updated with the latest communications.

### Concurrency and Asynchronous Operations

- **Reason**: Concurrency is achieved using asynchronous methods and tasks. The `StartListening` method uses `Task.Run` to avoid blocking the main thread while waiting for connections. The `HandleClient` method uses asynchronous read and write operations (`ReadAsync` and `WriteAsync`) to handle data transmission efficiently. This design ensures that the application can scale to handle many peers without performance degradation.

### Error Handling and Robustness

- **Reason**: Proper error handling is implemented to manage connection drops and other network-related issues. For example, when a client disconnects, it is removed from the `_clients` list, and the connection is closed. This approach ensures that the application remains robust and can recover gracefully from errors, maintaining the integrity of the P2P network.

### Summary

The design choices made for the P2P chat application ensure that it is scalable, efficient, and robust. By running each peer in a separate thread and using asynchronous methods for communication, the application can handle multiple connections and messages concurrently. Implementing methods for sending and receiving messages enables dynamic and direct peer-to-peer communication, which is essential for a decentralized network. These design choices collectively contribute to a responsive and reliable P2P chat application.


### Explanation of Design Choices for Section 5: Cryptography and Data Security

#### Basic Cryptography

1. **Implementing Encryption and Decryption Using AES**

   - **Use of AES (Advanced Encryption Standard)**
     - **Reason**: AES is a widely used and highly secure encryption standard. It is symmetric, meaning the same key is used for both encryption and decryption, making it suitable for scenarios where secure key exchange is feasible. Its robustness against various types of cryptographic attacks makes it an excellent choice for protecting sensitive data.

2. **Create Methods to Encrypt and Decrypt a String Using a Symmetric Key**

   - **Encryption Method: `Encrypt(string plainText)`**
     - **Key and IV Initialization**
       - **Reason**: The key and IV (Initialization Vector) are essential for the AES algorithm. The key is used to encrypt and decrypt data, while the IV ensures that the same plaintext does not always result in the same ciphertext, adding an additional layer of security.
       - **Base64 Encoding**: Converting the key and IV from Base64 strings ensures they are in the correct byte format for AES. This choice helps in securely storing and transmitting the key and IV as text, which can be easily managed.
     - **MemoryStream and CryptoStream**
       - **Reason**: Using `MemoryStream` allows us to handle data in memory, providing a flexible way to work with streams of bytes. `CryptoStream` integrates seamlessly with `MemoryStream` to apply cryptographic transformations during data read/write operations.
     - **StreamWriter**
       - **Reason**: `StreamWriter` is used to write the plaintext to the `CryptoStream`, which then gets encrypted and written to the `MemoryStream`. This layered approach ensures efficient and secure data handling.
     - **Base64 Encoding of Ciphertext**
       - **Reason**: Converting the encrypted byte array to a Base64 string makes it easy to store and transmit the ciphertext as a text string, ensuring compatibility with systems that handle textual data.

   - **Decryption Method: `Decrypt(string cipherText)`**
     - **Key and IV Initialization**
       - **Reason**: Just as with encryption, the key and IV must be converted back from Base64 strings to byte arrays. Using the same key and IV ensures the encrypted data can be correctly decrypted.
     - **MemoryStream and CryptoStream**
       - **Reason**: The `MemoryStream` is initialized with the Base64-decoded ciphertext. The `CryptoStream` is used to apply the decryption transformation as data is read from the `MemoryStream`.
     - **StreamReader**
       - **Reason**: `StreamReader` reads the decrypted data from the `CryptoStream` back into a plaintext string. This method ensures that the decryption process is efficient and the original plaintext is accurately recovered.

3. **Demonstrate the Encryption and Decryption of a Sample String**

   - **Sample Encryption and Decryption**
     - **Reason**: Demonstrating the encryption and decryption of a sample string verifies that the methods work correctly. It provides a practical example of how to use the encryption and decryption functions, making it easier for developers to understand and implement similar functionality in their applications.

### Summary

The design choices for implementing AES encryption and decryption focus on security, efficiency, and ease of use. By using Base64 encoding for keys and IVs, the implementation ensures these critical components can be managed as text. The layered approach using `MemoryStream`, `CryptoStream`, `StreamWriter`, and `StreamReader` provides a robust and flexible way to handle encryption and decryption processes. Demonstrating with a sample string validates the implementation and serves as a clear example for developers. This design ensures that the encrypted data is secure, the encryption/decryption processes are efficient, and the code is maintainable and easy to understand.


### Explanation of Design Choices for Section 6: IoT, P2P, and DLT Technologies

#### IoT Device Simulation

1. **Simulating an IoT Device**
   - **Generating Random Temperature Readings**
     - **Reason**: IoT devices often have sensors to capture data, such as temperature. In a simulation, random temperature readings mimic the variability of real-world data. This randomness helps in testing how the server handles different values and ensures that the system can cope with varying inputs, which is crucial for robustness and reliability.
     - **Implementation**: A random number generator can be used to produce temperature readings within a realistic range. This simulates the natural fluctuations in temperature that an actual sensor would capture.
   
   - **Sending Data to a Server Endpoint**
     - **Reason**: In a real IoT setup, devices communicate with servers to transmit data for processing, analysis, or storage. Sending data to a server endpoint in the simulation mimics this interaction and allows testing of the data transmission process, network reliability, and server-side handling of incoming data.
     - **Implementation**: The simulation would use HTTP requests (e.g., POST) to send the temperature data to a predefined server endpoint. This models how IoT devices typically use RESTful APIs to communicate with cloud services or local servers.

2. **Server-Side Implementation**
   - **Receiving Temperature Data**
     - **Reason**: The server needs to be able to accept incoming data from multiple IoT devices. This part of the implementation focuses on ensuring that the server can reliably receive and process incoming temperature readings, which is a core functionality in IoT ecosystems.
     - **Implementation**: An HTTP server endpoint (e.g., using a web framework like ASP.NET Core) can be set up to handle incoming POST requests containing the temperature data. This endpoint will parse the received data and prepare it for storage.
   
   - **Storing Temperature Data**
     - **Reason**: Storing the received data is crucial for later analysis, monitoring, and decision-making. Persistence of data ensures that it can be queried, visualized, and used for further processing, such as triggering alerts or generating reports.
     - **Implementation**: The server can store the data in a database (e.g., SQL, NoSQL). This decision depends on the requirements for data retrieval speed, scalability, and structure. A time-series database might be chosen for efficient storage and querying of time-stamped data, which is common in IoT applications.

### Summary

The design choices for simulating an IoT device and server interaction focus on realism, reliability, and scalability:

1. **Random Temperature Readings**: These simulate real-world sensor data, providing variability and ensuring the system can handle a wide range of inputs.
2. **Sending Data to a Server**: This models real IoT communication patterns, allowing for robust testing of data transmission and network interactions.
3. **Server Receiving and Storing Data**: The server's ability to accept and store data is critical for any IoT system. Proper implementation ensures data persistence, enabling future analysis and application functionalities.

These choices ensure a realistic simulation, providing a solid foundation for understanding and testing IoT ecosystems. The focus on realistic data generation and robust server-side handling ensures the system is both practical and scalable.
