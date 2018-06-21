Retrieve a Single Document
^^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve a single document by its ID.

Endpoint
--------

.. code-block:: none

    GET /api/v1/documentsinternal/{id}
    

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
| ``200 OK``          | ``DocumentInternal``      |                                                  |
|                     | object.                   |                                                  |
|                     |                           |                                                  |
|                     |                           |                                                  |
|                     |                           |                                                  |
|                     |                           |                                                  |
+---------------------+---------------------------+--------------------------------------------------+
| ``400 Bad Request`` | Description of the error. | * The description of the error is returned as an |    
|                     |                           |   object whose property is the name of the error |    
|                     |                           |   with a description of the error in the         |
|                     |                           |   related value.                                 |
|                     |                           |                                                  |
|                     |                           |   .. code-block:: javascript                     |
|                     |                           |                                                  | 
|                     |                           |       {                                          |
|                     |                           |           "Error": [                             |
|                     |                           |               "Description of the error."        | 
|                     |                           |           ]                                      |
|                     |                           |       }                                          |
|                     |                           |                                                  |   
+---------------------+---------------------------+--------------------------------------------------+
| ``404 Not Found``   | N/A                       | * This can mean the document does not have       |
|                     |                           |   an update with ``isPublished: true``.          |
|                     |                           |                                                  |
+---------------------+---------------------------+--------------------------------------------------+

Example
-------

.. code-block:: none

    GET /api/v1/documentsinternal/369

.. code-block:: javascript

    {
        "id": 369,
        "title": "Classic Portal User Guide",
        "product": "Classic Portal",
        "version": "V2018.3",
        "htmlLink": "ClassicPortal/UserGuide/HTML_V2018.3/index.html",
        "pdfLink": "ClassicPortal/UserGuide/PDF_V2018.3/ClassicPortal_UserGuide_V2018.3.pdf",
        "wordLink": "ClassicPortal/UserGuide/Word_V2018.3/ClassicPortal_UserGuide_V2018.3.docx",
        "otherLink": null,
        "isFitForClients": true,
        "shortDescription": "The document contains the full User Guide for the V2018.3 of Classic Portal",
        "documentType": "User Guide",
        "latestUpdate": "2019-02-01T00:00:00",
        "latestTopicsUpdated": "This is version 10 of the document.",
        "authors": [
            {
                "id": 3,
                "firstName": "Glen",
                "lastName": "Williams",
                "email": "gwilliams@company.com",
                "alias": "GWILL",
                "isFormerAuthor": false,
                "aitName": "Glen"
            },
            {
                "id": 2,
                "firstName": "Ariel",
                "lastName": "Taylor",
                "email": "ataylor@company.com",
                "alias": "ATAYL",
                "isFormerAuthor": false,
                "aitName": "Ariel"
            },
            {
                "id": 1,
                "firstName": "Jamie",
                "lastName": "Smith",
                "email": "jsmith@company.com",
                "alias": "JSMIT",
                "isFormerAuthor": false,
                "aitName": "Jamie"
            }
        ],
        "clientCatalogs": [
            {
                "id": 7,
                "name": "Classic Portal",
                "internalId": null
            },
            {
                "id": 9,
                "name": "Framework",
                "internalId": null
            }
        ]
    }