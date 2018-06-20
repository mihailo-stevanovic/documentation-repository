Search for Documents
^^^^^^^^^^^^^^^^^^^^

Retrieve a list of all published documents that match a search term. The document ``title``, ``shortDescription`` and ``latestTopicsUpdated`` are included in the search.

Endpoint
--------

.. code-block:: none

    GET /api/v1/documentsinternal/search?searchTerm={searchTerm}&limit={limit}&page={page}&exactMatch={exactMatch}
    

Request
-------

+-----------------+-------+---------+----------+--------------------------------------------------+
| Name            | Type  | Value   | Required | Description                                      |
+=================+=======+=========+==========+==================================================+
| ``searchTerm``  | query | string  | Yes      | Term used for searching for documents.           |
|                 |       |         |          |                                                  |
+-----------------+-------+---------+----------+--------------------------------------------------+
| ``limit``       | query | integer | No       | Number of returned results.                      |
|                 |       |         |          | Default is ``20``.                               |
+-----------------+-------+---------+----------+--------------------------------------------------+
| ``page``        | query | integer | No       | Index of the displayed set of results.           |
|                 |       |         |          | Default is ``1``.                                |
+-----------------+-------+---------+----------+--------------------------------------------------+
| ``exactMatch``  | query | boolean | No       | When set to ``true``, the search term is used as |
|                 |       |         |          | a whole. When set to ``false``, each word is     |
|                 |       |         |          | search for separately. Default is ``true``.      |
+-----------------+-------+---------+----------+--------------------------------------------------+

You can use the query parameters to implement server-side pagination. If you set ``limit`` to ``50`` and ``page`` to ``1``, the API retrieves the 50 most recently published documents. If you set ``limit`` to ``50`` and ``page`` to ``1``, second 50 most recently published documents display, and so on.

Response
--------

+---------------------+---------------------------+--------------------------------------------------+
| Status Code         | Body                      | Notes                                            |
+=====================+===========================+==================================================+
| ``200 OK``          | Array of                  | * The documents are first sorted by              |
|                     | ``DocumentInternal``      |   ``latestUpdate``, then by ``Product`` and      |
|                     | objects.                  |   finally by ``Version``.                        |
|                     |                           |                                                  |
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
| ``404 Not Found``   | N/A                       |                                                  |
|                     |                           |                                                  |
|                     |                           |                                                  |
+---------------------+---------------------------+--------------------------------------------------+

Example
-------

.. code-block:: none

    GET /api/v1/documentsinternal/search?searchTerm=admin%20nice&limit=3&page=1&exactMatch=true

.. code-block:: javascript

    [
        {
            "id": 2,
            "title": "Next Gen Portal Administrator Guide",
            "product": "Next Gen Portal",
            "version": "V2018.6",
            "htmlLink": "NextGenPortal/AdministratorGuide/HTML_V2018.6/index.html",
            "pdfLink": "NextGenPortal/AdministratorGuide/PDF_V2018.6/NextGenPortal_AdministratorGuide_V2018.6.pdf",
            "wordLink": "NextGenPortal/AdministratorGuide/Word_V2018.6/NextGenPortal_AdministratorGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": false,
            "shortDescription": "The document contains the full Administrator Guide for the V2018.6 of Next Gen Portal",
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
                    "id": 6,
                    "name": "Next Gen Portal",
                    "internalId": null
                }
            ]
        },
        {
            "id": 425,
            "title": "Classic Portal Administrator Guide",
            "product": "Classic Portal",
            "version": "V2018.6",
            "htmlLink": "ClassicPortal/AdministratorGuide/HTML_V2018.6/index.html",
            "pdfLink": "ClassicPortal/AdministratorGuide/PDF_V2018.6/ClassicPortal_AdministratorGuide_V2018.6.pdf",
            "wordLink": "ClassicPortal/AdministratorGuide/Word_V2018.6/ClassicPortal_AdministratorGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": false,
            "shortDescription": "The document contains the full Administrator Guide for the V2018.6 of Classic Portal",
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
                    "id": 7,
                    "name": "Classic Portal",
                    "internalId": null
                }
            ]
        },
        {
            "id": 11,
            "title": "Next Gen Portal Administrator Guide",
            "product": "Next Gen Portal",
            "version": "V2018.5",
            "htmlLink": "NextGenPortal/AdministratorGuide/HTML_V2018.5/index.html",
            "pdfLink": "NextGenPortal/AdministratorGuide/PDF_V2018.5/NextGenPortal_AdministratorGuide_V2018.5.pdf",
            "wordLink": "NextGenPortal/AdministratorGuide/Word_V2018.5/NextGenPortal_AdministratorGuide_V2018.5.docx",
            "otherLink": null,
            "isFitForClients": false,
            "shortDescription": "The document contains the full Administrator Guide for the V2018.5 of Next Gen Portal",
            "documentType": "Administrator Guide",
            "latestUpdate": "2019-06-01T00:00:00",
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
                    "id": 6,
                    "name": "Next Gen Portal",
                    "internalId": null
                }
            ]
        }
    ]