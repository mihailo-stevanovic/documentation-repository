Document Types
==============

**Document Types** are used to provide basic document classification based on the content (e.g. *User Guide*, *Release Notes*, *Installation Guide*, etc.). This enables end-users to quickly identify the document they need.

Each document has only one document type.

Data Model
^^^^^^^^^^

+--------------------+-----------------+---------------------------------------------+
| Property           | Type            | Description                                 |
+====================+=================+=============================================+
| id                 | integer         | Unique ID of the document type.             |
+--------------------+-----------------+---------------------------------------------+
| fullName           | string          | Name of the document type.                  |
+--------------------+-----------------+---------------------------------------------+
| shortName          | string          | Short name of the document type.            |
+--------------------+-----------------+---------------------------------------------+
| documentCategory   | integer         | Provides additional classification to the   |
|                    |                 | document type.                              |
|                    |                 |                                             |
|                    |                 | 0. Functional Documentation                 |
|                    |                 | 1. Technical Documentation                  |
|                    |                 | 2. Release Notes                            |
|                    |                 | 3. Other                                    |
+--------------------+-----------------+---------------------------------------------+


Create a Document Type
^^^^^^^^^^^^^^^^^^^^^^

You can create a single document type by executing a ``POST`` action against the ``api/v1/documenttypes`` endpoint.

.. code-block:: none

    POST api/v1/documenttypes


The body of the request requires a ``DocumentType`` object. You can set the ``id`` property to ``0`` or exclude it from the object.

.. code-block:: javascript

    {
        "fullName": "User Guide",
        "shortName": "UG",
        "documentCategory": 0
    }

If the document type is created successfully, a ``201 Created`` code is returned. The response body contains the newly created document type. 

.. code-block:: javascript

    {
        "id": 1,
        "fullName": "User Guide",
        "shortName": "UG",
        "documentCategory": 0
    }

If the document type cannot be saved, a ``400 Bad Request`` code is returned with a description of the error in the response body.

.. code-block:: javascript

    {
        "FullName": [
            "The FullName field is required."
        ],
        "ShortName": [
            "The Short name cannot be longer than 5 characters."
        ]
    }


Retrieving Existing Document Types
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve All Document Types
---------------------------

You can retrieve existing document types by executing a ``GET`` action against the ``api/v1/documenttypes`` endpoint.

.. code-block:: none

    GET api/v1/documenttypes

The ``200 OK`` status code is returned. The body of the response contains an array of all the ``DocumentType`` objects.

.. code-block:: javascript

    [
        {
            "id": 1,
            "fullName": "User Guide",
            "shortName": "UG",
            "documentCategory": 0
        },
        {
            "id": 6,
            "fullName": "Installation Guide",
            "shortName": "IG",
            "documentCategory": 1
        },  
        {
            "id": 9,
            "fullName": "Release Notes",
            "shortName": "RN",
            "documentCategory": 2
        }
    ]

If no document types are found, a ``404 Not Found`` status code is returned.


Retrieve a Single Document Type
-------------------------------

You can also retrieve a single document type by executing a ``GET`` action against the ``api/v1/documenttypes/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the document type.

.. code-block:: none

    GET api/v1/documenttypes/1

The ``200 OK`` status code is returned. The body of the response contains a single ``DocumentType`` object.

.. code-block:: javascript

    {
        "id": 1,
        "fullName": "User Guide",
        "shortName": "UG",
        "documentCategory": 0
    }

If a document type with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

.. _put-documenttype:

Update a Document Type
^^^^^^^^^^^^^^^^^^^^^^

You can modify an existing document type by executing a ``PUT`` action against the ``api/v1/documenttypes/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the document type. 

.. code-block:: none

    PUT api/v1/documenttypes/1

Use the request body to pass the updated ``DocumentType`` object. Please note that you need to include all the properties of the object, including the ``ID``.

.. code-block:: javascript

    {
        "id": 1,
        "fullName": "User Guide",
        "shortName": "UG",
        "documentCategory": 0
    }

If the document type is updated successfully, a ``204 No Content`` code is returned.

If the request was incorrect in any way, a ``400 Bad Request`` status code is returned, with the description of the error in the response body.

.. code-block:: javascript

    {
        "Invalid Document Type ID": [
            "The Document Type ID supplied in the query and the body of the request do not match."
        ]
    }

If a document type with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

Remove a Document Type
^^^^^^^^^^^^^^^^^^^^^^^

In some cases, you may want to delete a document type from the database. You can achieve this by executing a ``DELETE`` action against the ``api/v1/documenttypes/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the ``DocumentType`` object.

.. warning:: Due to a `bug <https://github.com/mihailo-stevanovic/documentation-repository/issues/2>`_, removing a document type currently also removes all the related documents.

.. code-block:: none

    DELETE api/v1/documenttypes/1

The ``200 OK`` status code is returned. The body of the response contains the deleted ``DocumentType`` object.

.. code-block:: javascript

    {
        "id": 1,
        "fullName": "User Guide",
        "shortName": "UG",
        "documentCategory": 0
    }

If a document type with a matching ID cannot be found, a ``404 Not Found`` status code is returned.