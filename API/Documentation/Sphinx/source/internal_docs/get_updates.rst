Retrieve a Document Updates
^^^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve all the published updates related to a single document.

Endpoint
--------

.. code-block:: none

    GET /api/v1/documentsinternal/{id}/updates
    

Request
-------

+-----------------+-------+---------+----------+--------------------------------------------------+
| Name            | Type  | Value   | Required | Description                                      |
+=================+=======+=========+==========+==================================================+
| ``id``          | path  | integer | Yes      | ID of the document.                              |
|                 |       |         |          |                                                  |
+-----------------+-------+---------+----------+--------------------------------------------------+

Response
--------

+---------------------+---------------------------+--------------------------------------------------+
| Status Code         | Body                      | Notes                                            |
+=====================+===========================+==================================================+
| ``200 OK``          | Array of                  | * The updates are sorted by                      |
|                     | ``DocumentUpdate``        |   ``timestamp``.                                 |
|                     | objects.                  |                                                  |
|                     |                           | * Only documents with at least one update that   |
|                     |                           |   has ``isPublished: true`` are retrieved.       |
|                     |                           |                                                  |
+---------------------+---------------------------+--------------------------------------------------+
| ``400 Bad Request`` | Description of the error. | * The description of the error is returned as an |    
|                     |                           |   object whose property is the name of the error |    
|                     |                           |   and value is a description of the error.       |
|                     |                           |                                                  |
|                     |                           |                                                  | 
|                     |                           |   .. code-block:: javascript                     |
|                     |                           |                                                  | 
|                     |                           |       {                                          |
|                     |                           |           "Error": [                             |
|                     |                           |               "Description of the error."        | 
|                     |                           |           ]                                      |
|                     |                           |       }                                          |    
+---------------------+---------------------------+--------------------------------------------------+
| ``404 Not Found``   | N/A                       | * This can mean the document does not have       |
|                     |                           |   an update with ``isPublished: true`` or that   |
|                     |                           |   the ``id`` of the document is incorrect.       |
+---------------------+---------------------------+--------------------------------------------------+

Example
-------

.. code-block:: none

    GET /api/v1/documentsinternal/369/updates

.. code-block:: javascript

    [
        {
            "id": 3404,
            "timestamp": "2019-02-01T00:00:00",
            "latestTopicsUpdated": "This is version 10 of the document.",
            "isPublished": true,
            "documentId": 369,
            "rowVersion": "AAAAAAAAGI0="
        },
        {
            "id": 3421,
            "timestamp": "2018-12-01T00:00:00",
            "latestTopicsUpdated": "This is version 8 of the document.",
            "isPublished": true,
            "documentId": 369,
            "rowVersion": "AAAAAAAAGJ4="
        },
        {
            "id": 3407,
            "timestamp": "2018-11-01T00:00:00",
            "latestTopicsUpdated": "This is version 7 of the document.",
            "isPublished": true,
            "documentId": 369,
            "rowVersion": "AAAAAAAAGJA="
        },
        {
            "id": 3409,
            "timestamp": "2018-09-01T00:00:00",
            "latestTopicsUpdated": "This is version 5 of the document.",
            "isPublished": true,
            "documentId": 369,
            "rowVersion": "AAAAAAAAGJI="
        },
        {
            "id": 3410,
            "timestamp": "2018-08-01T00:00:00",
            "latestTopicsUpdated": "This is version 4 of the document.",
            "isPublished": true,
            "documentId": 369,
            "rowVersion": "AAAAAAAAGJM="
        },
        {
            "id": 3412,
            "timestamp": "2018-06-01T00:00:00",
            "latestTopicsUpdated": "This is version 2 of the document.",
            "isPublished": true,
            "documentId": 369,
            "rowVersion": "AAAAAAAAGJU="
        },
        {
            "id": 3413,
            "timestamp": "2018-05-01T00:00:00",
            "latestTopicsUpdated": "This is version 1 of the document.",
            "isPublished": true,
            "documentId": 369,
            "rowVersion": "AAAAAAAAGJY="
        }
    ]