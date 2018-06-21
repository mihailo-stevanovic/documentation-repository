Retrieve By Product
^^^^^^^^^^^^^^^^^^^

Retrieve a list of all published documents of a specified product.

Endpoint
--------

.. code-block:: none

    GET /api/v1/documentsinternal/byproduct/{productId}?limit={limit}&page={page}
    

Request
-------

+-----------------+-------+---------+----------+--------------------------------------------------+
| Name            | Type  | Value   | Required | Description                                      |
+=================+=======+=========+==========+==================================================+
| ``productId``   | path  | integer | Yes      | ID of the related product.                       |
|                 |       |         |          |                                                  |
+-----------------+-------+---------+----------+--------------------------------------------------+
| ``limit``       | query | integer | No       | Number of returned results.                      |
|                 |       |         |          |                                                  |
|                 |       |         |          | Default is ``20``.                               |
+-----------------+-------+---------+----------+--------------------------------------------------+
| ``page``        | query | integer | No       | Index of the displayed set of results.           |
|                 |       |         |          |                                                  |
|                 |       |         |          | Default is ``1``.                                |
+-----------------+-------+---------+----------+--------------------------------------------------+

.. include:: _query_param_pagination.rst

Response
--------

+---------------------+---------------------------+--------------------------------------------------+
| Status Code         | Body                      | Notes                                            |
+=====================+===========================+==================================================+
| ``200 OK``          | Array of                  | * The documents are first sorted by              |
|                     | ``DocumentInternal``      |   ``latestUpdate``, then by ``version``.         |
|                     | objects.                  |                                                  |
|                     |                           | * Only documents with at least one update that   |
|                     |                           |   has ``isPublished: true`` are retrieved.       |
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
| ``404 Not Found``   | N/A                       | * This can mean that ``productId`` is incorrect. |
|                     |                           |                                                  |
|                     |                           |                                                  |
+---------------------+---------------------------+--------------------------------------------------+

Example
-------

.. code-block:: none

    GET /api/v1/documentsinternal/byproduct/1?limit=3&page=1

.. code-block:: javascript

    [
        {
            "id": 434,
            "title": "Nice Product Configuration Guide",
            "product": "Nice Product",
            "version": "V2018.6",
            "htmlLink": "NiceProduct/ConfigurationGuide/HTML_V2018.6/index.html",
            "pdfLink": "NiceProduct/ConfigurationGuide/PDF_V2018.6/NiceProduct_ConfigurationGuide_V2018.6.pdf",
            "wordLink": "NiceProduct/ConfigurationGuide/Word_V2018.6/NiceProduct_ConfigurationGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": false,
            "shortDescription": "The document contains the full Configuration Guide for the V2018.6 of Nice Product",
            "documentType": "Configuration Guide",
            "latestUpdate": "2019-08-01T00:00:00",
            "latestTopicsUpdated": "This is version 10 of the document.",
            "authors": [
                {
                    "id": 1,
                    "firstName": "Jamie",
                    "lastName": "Smith",
                    "email": "jsmith@company.com",
                    "alias": "JSMIT",
                    "isFormerAuthor": false,
                    "aitName": "Jamie"
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
                    "id": 3,
                    "firstName": "Glen",
                    "lastName": "Williams",
                    "email": "gwilliams@company.com",
                    "alias": "GWILL",
                    "isFormerAuthor": false,
                    "aitName": "Glen"
                }
            ],
            "clientCatalogs": [
                {
                    "id": 2,
                    "name": "Nice Product",
                    "internalId": null
                },
                {
                    "id": 9,
                    "name": "Framework",
                    "internalId": null
                }
            ]
        },
        {
            "id": 435,
            "title": "Nice Product Administrator Guide",
            "product": "Nice Product",
            "version": "V2018.6",
            "htmlLink": "NiceProduct/AdministratorGuide/HTML_V2018.6/index.html",
            "pdfLink": "NiceProduct/AdministratorGuide/PDF_V2018.6/NiceProduct_AdministratorGuide_V2018.6.pdf",
            "wordLink": "NiceProduct/AdministratorGuide/Word_V2018.6/NiceProduct_AdministratorGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": false,
            "shortDescription": "The document contains the full Administrator Guide for the V2018.6 of Nice Product",
            "documentType": "Administrator Guide",
            "latestUpdate": "2019-08-01T00:00:00",
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
                    "id": 9,
                    "name": "Framework",
                    "internalId": null
                },
                {
                    "id": 2,
                    "name": "Nice Product",
                    "internalId": null
                }
            ]
        },
        {
            "id": 436,
            "title": "Nice Product Reference Guide",
            "product": "Nice Product",
            "version": "V2018.6",
            "htmlLink": "NiceProduct/ReferenceGuide/HTML_V2018.6/index.html",
            "pdfLink": "NiceProduct/ReferenceGuide/PDF_V2018.6/NiceProduct_ReferenceGuide_V2018.6.pdf",
            "wordLink": "NiceProduct/ReferenceGuide/Word_V2018.6/NiceProduct_ReferenceGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": true,
            "shortDescription": "The document contains the full Reference Guide for the V2018.6 of Nice Product",
            "documentType": "Reference Guide",
            "latestUpdate": "2019-08-01T00:00:00",
            "latestTopicsUpdated": "This is version 10 of the document.",
            "authors": [
                {
                    "id": 1,
                    "firstName": "Jamie",
                    "lastName": "Smith",
                    "email": "jsmith@company.com",
                    "alias": "JSMIT",
                    "isFormerAuthor": false,
                    "aitName": "Jamie"
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
                    "id": 3,
                    "firstName": "Glen",
                    "lastName": "Williams",
                    "email": "gwilliams@company.com",
                    "alias": "GWILL",
                    "isFormerAuthor": false,
                    "aitName": "Glen"
                }
            ],
            "clientCatalogs": [
                {
                    "id": 2,
                    "name": "Nice Product",
                    "internalId": null
                },
                {
                    "id": 9,
                    "name": "Framework",
                    "internalId": null
                }
            ]
        }
    ]