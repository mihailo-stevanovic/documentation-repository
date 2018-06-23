Product Versions
================

A **Product Version** represents a single release of a :doc:`product <products>`. It establishes a link between documents and products and groups all the documents related to a product release. A document can only have one product version.

In the |API|, versions are accessible through the related products. The base endpoint is ``/api/v1/products/{productId}/versions`` where ``{productId}`` refers to the ID of the product.

Data Model
^^^^^^^^^^

+--------------------+-----------------+---------------------------------------------+
| Property           | Type            | Description                                 |
+====================+=================+=============================================+
| id                 | integer         | Unique ID of the product.                   |
+--------------------+-----------------+---------------------------------------------+
| product            | string          | Marketing name of the related product.      |
+--------------------+-----------------+---------------------------------------------+
| release            | string          | Release version of the product.             |
+--------------------+-----------------+---------------------------------------------+
| endOfSupport       | datetime        | Date until the product is supported.        |
+--------------------+-----------------+---------------------------------------------+
| isSupported        | boolean         | Read-only. Determines if the version is     |
|                    |                 | still supported. Can be used to display in  |
|                    |                 | the portal that the document is linked to   |
|                    |                 | an unsupported version.                     |
+--------------------+-----------------+---------------------------------------------+

Creating Product Versions
^^^^^^^^^^^^^^^^^^^^^^^^^

Create a Single Product Version
-------------------------------

You can create a single version of a product by executing a ``POST`` action against the ``api/v1/products/{productId}/versions`` endpoint.

.. code-block:: none

    POST api/v1/products/{productId}/versions


The body of the request requires a ``ProductVersion`` object. You can set the ``id`` property to ``0`` or exclude it from the object. You can also omit the ``product`` property because the product link is created from the ``productId`` path parameter.

.. note:: The ``isSupported`` property is read-only and should not included in the request body.


.. code-block:: javascript

    {
        "release": "V2018.3",
        "endOfSupport": "2019-05-01T00:00:00"
    }

If the product version is created successfully, a ``201 Created`` code is returned. The response body contains the newly created product version. 

.. code-block:: javascript

    {
        "id": 1,
        "product": "Awesome Product",
        "release": "V2018.3",
        "endOfSupport": "2019-05-01T00:00:00",
        "isSupported": true
    }

If the product version cannot be saved, a ``400 Bad Request`` code is returned with a description of the error in the response body.

.. code-block:: javascript

    {
        "endOfSupport": [
            "The input was not valid."
        ]
    }

Create Multiple Product Versions
--------------------------------

If you are migrating to the *Documentation Repository* from another tool or you know your full release schedule in advance, it may be useful to create product versions in bulk. You can achieve this by executing a ``POST`` action against the ``api/v1/products/{productId}/versions/batch`` endpoint.

.. code-block:: none

    POST api/v1/products/{productId}/versions/batch

The body of the request accepts an array of ``ProductVersion`` objects. You can set the ``id`` property to ``0`` or exclude it from the object. You can also omit the ``product`` property because the product link is created from the ``productId`` path parameter. 

.. note:: The ``isSupported`` property is read-only and should not included in the request body.

.. code-block:: javascript

    [
        {
            "release": "V2018.3",
            "endOfSupport": "2019-05-01T00:00:00"
        },
        {
            "release": "V2018.4",
            "endOfSupport": "2019-07-01T00:00:00"
        }
    ]

If the versions are created successfully, a ``201 Created`` code is returned. The response body contains an array of the newly created product versions. 

.. code-block:: javascript

    [
        {
            "id": 1,
            "product": "Awesome Product",
            "release": "V2018.3",
            "endOfSupport": "2019-05-01T00:00:00",
            "isSupported": true
        },
        {
            "id": 2,
            "product": "Awesome Product",
            "release": "V2018.4",
            "endOfSupport": "2019-07-01T00:00:00",
            "isSupported": true
        }
    ]

If a product version cannot be saved, a ``400 Bad Request`` code is returned with a description of the error in the response body.

.. code-block:: javascript

    {
        "[0].Release": [
            "The Release cannot be longer than 10 characters."
        ],
        "[1].Release": [
            "The Release field is required."
        ]
    }

Retrieving Existing Product Versions
^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

Retrieve All Product Versions
-----------------------------

You can retrieve existing versions by executing a ``GET`` action against the ``api/v1/products/{productId}/versions`` endpoint.

.. code-block:: none

    GET api/v1/products/{productId}/versions

The ``200 OK`` status code is returned. The body of the response contains an array of all the ``ProductVersion`` objects sorted by ``release`` in descending order.

.. code-block:: javascript

    [
        {
            "id": 2,
            "product": "Awesome Product",
            "release": "V2018.4",
            "endOfSupport": "2019-07-01T00:00:00",
            "isSupported": true
        },
        {
            "id": 1,
            "product": "Awesome Product",
            "release": "V2018.3",
            "endOfSupport": "2019-05-01T00:00:00",
            "isSupported": true
        }
    ]


If no products are found, a ``404 Not Found`` status code is returned.


Retrieve a Single Product Version
---------------------------------

You can also retrieve a single product version by executing a ``GET`` action against the ``api/v1/products/{productId}/versions/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the product version.

.. code-block:: none

    GET api/v1/products/{productId}/versions/1

The ``200 OK`` status code is returned. The body of the response contains a single ``ProductVersion`` object.

.. code-block:: javascript

    {
        "product": "Awesome Product",
        "release": "V2018.3",
        "endOfSupport": "2019-05-01T00:00:00",
        "isSupported": true
    }

If a product version with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

.. _put-productversion:

Update a Product Version
^^^^^^^^^^^^^^^^^^^^^^^^

You can modify an existing product version by executing a ``PUT`` action against the ``api/v1/products/{productId}/versions/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the product version.

.. code-block:: none

    PUT api/v1/products/{productId}/versions/1

Use the request body to pass the updated ``ProductVersion`` object. You can omit the ``product`` property. 

.. note:: The ``isSupported`` property is read-only and should not included in the request body.

.. code-block:: javascript
    :emphasize-lines: 3

    {
        "id": 1,
        "release": "V2018.3",
        "endOfSupport": "2019-05-31T00:00:00",
    }

If the product version is updated successfully, a ``204 No Content`` code is returned.

If the request was incorrect in any way, a ``400 Bad Request`` status code is returned, with the description of the error in the response body.

.. code-block:: javascript

    {
        "Invalid Version ID": [
            "The Version ID supplied in the query and the body of the request do not match."
        ]
    }

If a product version with a matching ID is cannot be found, a ``404 Not Found`` status code is returned.

Remove a Product Version
^^^^^^^^^^^^^^^^^^^^^^^^

In some cases, you may want to delete a product version from the database. You can achieve this by executing a ``DELETE`` action against the ``api/v1/products/{productId}/versions/{id}`` endpoint where the ``{id}`` parameter refers to the ID of the ``ProductVersion`` object.

.. warning:: Due to a `bug <https://github.com/mihailo-stevanovic/documentation-repository/issues/3>`_, removing a product version currently also removes all the related documents.

.. code-block:: none

    DELETE api/v1/products/{productId}/versions/1

The ``200 OK`` status code is returned. The body of the response contains the deleted ``ProductVersion`` object.

.. code-block:: javascript

    {
        "id": 1,
        "product": "Awesome Product",
        "release": "V2018.3",
        "endOfSupport": "2019-05-31T00:00:00",
        "isSupported": true
    }

If a product version with a matching ID cannot be found, a ``404 Not Found`` status code is returned.