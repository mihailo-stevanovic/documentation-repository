Client Catalogs
===============

Each **Client Catalog** entity represents a subportal that is used to display documents to external users (the company's clients). This ensures that each client can only see the information they have access to. 

Client catalogs have a reduced scope compared to the internal portal. They include less documents and less information about a specific document. They should be regarded as points of entry. 

A client catalog is typically associated to a single product (e.g. *Nice Product*) or a subset of documents (e.g. *Nice Product Release Notes*).

Data Model
^^^^^^^^^^

+--------------------+-----------------+---------------------------------------------+
| Property           | Type            | Description                                 |
+====================+=================+=============================================+
| id                 | integer         | Unique ID of the client catalog.            |
+--------------------+-----------------+---------------------------------------------+
| name               | string          | Name of the client catalog.                 |
+--------------------+-----------------+---------------------------------------------+
| internalId         | string          | ID of the client catalog in another         |
|                    |                 | internal system (e.g. the company's client  |
|                    |                 | support system). Used for integration.      |
+--------------------+-----------------+---------------------------------------------+


Creating Client Catalogs
^^^^^^^^^^^^^^^^^^^^^^^^

Create a Single Client Catalog
------------------------------

You can create a single client catalog by executing a ``POST`` action against the ``api/v1/clientcatalogs`` endpoint.

.. code-block:: none

    POST api/v1/clientcatalogs


The body of the request requires a ``ClientCatalog`` object. You can set the ``id`` property to ``0`` or exclude it from the object.

.. code-block:: javascript

    {
        "name": "Awesome Product"        
    }

If the catalog is created successfully, a ``201 Created`` code is returned. The response body contains the newly created client catalog. 

.. code-block:: javascript

    {
        "id": 1,
        "name": "Awesome Product",
        "internalId": null
    }

If the catalog cannot be saved, a ``400 Bad Request`` code is returned with a description of the error in the response body.

.. code-block:: javascript

    {
        "Name": [
            "The Name field is required."
        ]
    }

Create Multiple Client Catalogs
-------------------------------

If you are migrating to the *Documentation Repository* from another tool, it may be useful to create client catalogs in bulk. You can achieve this by executing a ``POST`` action against the ``api/v1/clientcatalogs/batch`` endpoint.

.. code-block:: none

    POST api/v1/clientcatalogs/batch

The body of the request accepts an array of ``ClientCatalog`` objects. You can set the ``id`` property to ``0`` or exclude it entirely.

.. code-block:: javascript

    [
        {
            "name": "Awesome Product" 
        },
        {
            "name": "Nice Product" 
        }
    ]

If the catalogs are created successfully, a ``201 Created`` code is returned. The response body contains an array of the newly created client catalogs. 

.. code-block:: javascript

    [
        {
            "id": 1,
            "name": "Awesome Product",
            "internalId": null
        },
        {
            "id": 2,
            "name": "Nice Product",
            "internalId": null
        }
    ]

If a catalog cannot be saved, a ``400 Bad Request`` code is returned with a description of the error in the response body.

.. code-block:: javascript

    {
        "[0].Name": [
            "The Name field is required."
        ],
        "[1].Name": [
            "The Name field is required."
        ]
    }


Retrieving Existing Client Catalogs
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve All Client Catalogs
----------------------------

You can retrieve existing client catalogs by executing a ``GET`` action against the ``api/v1/clientcatalogs`` endpoint.

.. code-block:: none

    GET api/v1/clientcatalogs

The ``200 OK`` status code is returned. The body of the response contains an array of all the ``ClientCatalog`` objects.

.. code-block:: javascript

    [
        {
            "id": 1,
            "name": "Awesome Product",
            "internalId": null
        },
        {
            "id": 2,
            "name": "Nice Product",
            "internalId": null
        },
        {
            "id": 3,
            "name": "Old Product",
            "internalId": null
        }
    ]

If no client catalogs are found, a ``404 Not Found`` status code is returned.

Retrieve a Single Client Catalog
---------------------------------

You can also retrieve a single client catalog by executing a ``GET`` action against the ``api/v1/clientcatalogs/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the client catalog.

.. code-block:: none

    GET api/v1/clientcatalogs/1

The ``200 OK`` status code is returned. The body of the response contains a single ``ClientCatalog`` object.

.. code-block:: javascript

    {
        "id": 1,
        "name": "Awesome Product",
        "internalId": null
    }

If a client catalog with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

.. _put-clientcatalog:

Update a Client Catalog
^^^^^^^^^^^^^^^^^^^^^^^

You can modify an existing client catalog by executing a ``PUT`` action against the ``api/v1/clientcatalogs/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the client catalog.

.. code-block:: none

    PUT api/v1/clientcatalogs/1

Use the request body to pass the updated ``ClientCatalog`` object. Please note that you need to include all the properties of the object, including the ``ID``.

.. code-block:: javascript
    :emphasize-lines: 4

    {
        "id": 1,
        "name": "Awesome Product",
        "internalId": "6F9619FF-8B86-D011-B42D-00C04FC964FF"
    }

If the client catalog is updated successfully, a ``204 No Content`` code is returned.

If the request was incorrect in any way, a ``400 Bad Request`` status code is returned, with the description of the error in the response body.

.. code-block:: javascript

    {
        "Invalid Catalog ID": [
            "The Catalog ID supplied in the query and the body of the request do not match."
        ]
    }

If a client catalog with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

Remove a Client Catalog
^^^^^^^^^^^^^^^^^^^^^^^

In some cases, you may want to delete a client catalog from the database. You can achieve this by executing a ``DELETE`` action against the ``api/v1/clientcatalogs/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the ``ClientCatalog`` object.

.. warning:: Removing a client catalog also removes all the references to it. This may cause documents to have no client catalogs attached. To avoid this, you may want to consider :ref:`updating the client catalog <put-clientcatalog>` instead.

.. code-block:: none

    DELETE api/v1/clientcatalogs/1

The ``200 OK`` status code is returned. The body of the response contains the deleted ``ClientCatalog`` object.

.. code-block:: javascript

    {
        "id": 1,
        "name": "Awesome Product",
        "internalId": "6F9619FF-8B86-D011-B42D-00C04FC964FF"
    }

If a client catalog with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.