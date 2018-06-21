Retrieve Published Documents
^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve a list of all the published documents.

Endpoint
--------

.. code-block:: none

    GET /api/v1/documentsinternal?limit={limit}&page={page}

Request
-------

+-----------------+-------+---------+----------+--------------------------------------------------+
| Name            | Type  | Value   | Required | Description                                      |
+=================+=======+=========+==========+==================================================+
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
|                     | ``DocumentInternal``      |   ``latestUpdate``, then by ``product`` and      |
|                     | objects.                  |   finally by ``version``.                        |
|                     |                           |                                                  |
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
| ``404 Not Found``   | N/A                       |                                                  |
|                     |                           |                                                  |
|                     |                           |                                                  |
+---------------------+---------------------------+--------------------------------------------------+

Example
-------

.. code-block:: none

    GET /api/v1/documentsinternal?limit=5&page=1

.. code-block:: javascript   

    [
        {
            "id": 542,
            "title": "Awesome Product Configuration Guide",
            "product": "Awesome Product",
            "version": "V2018.6",
            "htmlLink": "AwesomeProduct/ConfigurationGuide/HTML_V2018.6/index.html",
            "pdfLink": "AwesomeProduct/ConfigurationGuide/PDF_V2018.6/AwesomeProduct_ConfigurationGuide_V2018.6.pdf",
            "wordLink": "AwesomeProduct/ConfigurationGuide/Word_V2018.6/AwesomeProduct_ConfigurationGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": false,
            "shortDescription": "The document contains the full Configuration Guide for the V2018.6 of Awesome Product",
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
                }
            ],
            "clientCatalogs": [
                {
                    "id": 1,
                    "name": "Awesome Product",
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
            "id": 543,
            "title": "Awesome Product Administrator Guide",
            "product": "Awesome Product",
            "version": "V2018.6",
            "htmlLink": "AwesomeProduct/AdministratorGuide/HTML_V2018.6/index.html",
            "pdfLink": "AwesomeProduct/AdministratorGuide/PDF_V2018.6/AwesomeProduct_AdministratorGuide_V2018.6.pdf",
            "wordLink": "AwesomeProduct/AdministratorGuide/Word_V2018.6/AwesomeProduct_AdministratorGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": false,
            "shortDescription": "The document contains the full Administrator Guide for the V2018.6 of Awesome Product",
            "documentType": "Administrator Guide",
            "latestUpdate": "2019-08-01T00:00:00",
            "latestTopicsUpdated": "This is version 10 of the document.",
            "authors": [
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
                    "id": 1,
                    "name": "Awesome Product",
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
            "id": 544,
            "title": "Awesome Product Reference Guide",
            "product": "Awesome Product",
            "version": "V2018.6",
            "htmlLink": "AwesomeProduct/ReferenceGuide/HTML_V2018.6/index.html",
            "pdfLink": "AwesomeProduct/ReferenceGuide/PDF_V2018.6/AwesomeProduct_ReferenceGuide_V2018.6.pdf",
            "wordLink": "AwesomeProduct/ReferenceGuide/Word_V2018.6/AwesomeProduct_ReferenceGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": true,
            "shortDescription": "The document contains the full Reference Guide for the V2018.6 of Awesome Product",
            "documentType": "Reference Guide",
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
                    "id": 1,
                    "name": "Awesome Product",
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
            "id": 545,
            "title": "Awesome Product Knowledge Base",
            "product": "Awesome Product",
            "version": "V2018.6",
            "htmlLink": "AwesomeProduct/KnowledgeBase/HTML_V2018.6/index.html",
            "pdfLink": "AwesomeProduct/KnowledgeBase/PDF_V2018.6/AwesomeProduct_KnowledgeBase_V2018.6.pdf",
            "wordLink": "AwesomeProduct/KnowledgeBase/Word_V2018.6/AwesomeProduct_KnowledgeBase_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": true,
            "shortDescription": "The document contains the full Knowledge Base for the V2018.6 of Awesome Product",
            "documentType": "Knowledge Base",
            "latestUpdate": "2019-08-01T00:00:00",
            "latestTopicsUpdated": "This is version 10 of the document.",
            "authors": [
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
                    "id": 1,
                    "name": "Awesome Product",
                    "internalId": null
                }
            ]
        },
        {
            "id": 546,
            "title": "Awesome Product Installation Guide",
            "product": "Awesome Product",
            "version": "V2018.6",
            "htmlLink": "AwesomeProduct/InstallationGuide/HTML_V2018.6/index.html",
            "pdfLink": "AwesomeProduct/InstallationGuide/PDF_V2018.6/AwesomeProduct_InstallationGuide_V2018.6.pdf",
            "wordLink": "AwesomeProduct/InstallationGuide/Word_V2018.6/AwesomeProduct_InstallationGuide_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": true,
            "shortDescription": "The document contains the full Installation Guide for the V2018.6 of Awesome Product",
            "documentType": "Installation Guide",
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
                }
            ],
            "clientCatalogs": [
                {
                    "id": 9,
                    "name": "Framework",
                    "internalId": null
                },
                {
                    "id": 1,
                    "name": "Awesome Product",
                    "internalId": null
                }
            ]
        }
    ]
