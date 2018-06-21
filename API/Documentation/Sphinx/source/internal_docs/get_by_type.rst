Retrieve By Document Type
^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve a list of all published documents of a specified document type. Useful for filtering documents based on the target audience. E.g. end-users may only be interested in User Guides while the technical support may want to view only Installation Guides.

Endpoint
--------

.. code-block:: none

    GET /api/v1/documentsinternal/bydoctype/{docTypeId}?limit={limit}&page={page}
    

Request
-------

+-----------------+-------+---------+----------+--------------------------------------------------+
| Name            | Type  | Value   | Required | Description                                      |
+=================+=======+=========+==========+==================================================+
| ``docTypeId``   | path  | integer | Yes      | ID of the related document type.                 |
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
| ``404 Not Found``   | N/A                       | * This can mean that ``docTypeId`` is incorrect. |
|                     |                           |                                                  |
|                     |                           |                                                  |
+---------------------+---------------------------+--------------------------------------------------+

Example
-------

.. code-block:: none

    GET /api/v1/documentsinternal/bydoctype/9?limit=3&page=1

.. code-block:: javascript

    [
        {
            "id": 549,
            "title": "Awesome Product Release Notes",
            "product": "Awesome Product",
            "version": "V2018.6",
            "htmlLink": "AwesomeProduct/ReleaseNotes/HTML_V2018.6/index.html",
            "pdfLink": "AwesomeProduct/ReleaseNotes/PDF_V2018.6/AwesomeProduct_ReleaseNotes_V2018.6.pdf",
            "wordLink": "AwesomeProduct/ReleaseNotes/Word_V2018.6/AwesomeProduct_ReleaseNotes_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": true,
            "shortDescription": "The document contains the full Release Notes for the V2018.6 of Awesome Product",
            "documentType": "Release Notes",
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
            "id": 430,
            "title": "Classic Portal Release Notes",
            "product": "Classic Portal",
            "version": "V2018.6",
            "htmlLink": "ClassicPortal/ReleaseNotes/HTML_V2018.6/index.html",
            "pdfLink": "ClassicPortal/ReleaseNotes/PDF_V2018.6/ClassicPortal_ReleaseNotes_V2018.6.pdf",
            "wordLink": "ClassicPortal/ReleaseNotes/Word_V2018.6/ClassicPortal_ReleaseNotes_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": true,
            "shortDescription": "The document contains the full Release Notes for the V2018.6 of Classic Portal",
            "documentType": "Release Notes",
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
                    "id": 7,
                    "name": "Classic Portal",
                    "internalId": null
                }
            ]
        },
        {
            "id": 116,
            "title": "CRM Solution Release Notes",
            "product": "CRM Solution",
            "version": "V2018.6",
            "htmlLink": "CRMSolution/ReleaseNotes/HTML_V2018.6/index.html",
            "pdfLink": "CRMSolution/ReleaseNotes/PDF_V2018.6/CRMSolution_ReleaseNotes_V2018.6.pdf",
            "wordLink": "CRMSolution/ReleaseNotes/Word_V2018.6/CRMSolution_ReleaseNotes_V2018.6.docx",
            "otherLink": null,
            "isFitForClients": true,
            "shortDescription": "The document contains the full Release Notes for the V2018.6 of CRM Solution",
            "documentType": "Release Notes",
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
                    "id": 5,
                    "name": "CRM Solution",
                    "internalId": null
                }
            ]
        }
    ]