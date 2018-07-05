# Documentation Repository

The **Documentation Repository** can be used as a limited publishing platform for software companies that have a large repository of different types of technical documents across multiple products and releases. It is not used for storing documents, but rather as a portal with references for accessing the documents.

## Table of Contents

- [About](#about)
- [API](#api)
- [Portal](#portal)
- [Admin](#admin)

## About

This is a **test project** that I created for two reasons:

1. To learn and experiment with various technologies and techniques.

2. To create a sample of my technical writing.

The project was never intended to be used in real production and was, subsequently, never properly tested. However, if you find it useful in any way, feel free to use it.

Please note that I am not a coder and have probably made many mistakes. For me, it's all about exploring and having fun. However, if you do have any suggestions, I would be more than happy to hear them.

## API

The center of the project is a ASP.NET Core 2.0 Web Application with Entity Framework. It exposes all the main entities through a RESTful API.

It implements Swagger documentation using Swashbuckle.

### Limitations

- **No authorization** - the API isn't currently protected by an authorization mechanism.

- **Incomplete client catalog implementation** - I have yet to implement an external document object and provide an endpoint for retrieving client facing documents.

## Portal

The portal app is built using React.js and Material UI. It is used to display all documents to internal users (company employees).

For more information, please refer to the dedicated [README](portal/README.md).

## Admin

The admin app is the final piece. It will expose all the administrative actions in web application.
