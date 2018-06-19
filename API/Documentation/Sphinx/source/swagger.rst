Using the Swagger Documentation
===============================

In addition to this document, the |API| contains Swagger documentation that should be used as a Reference Guide. It comes in two versions:

1. Automatically generated document from the source code using Swashbuckle. It is based on the XML descriptions and comments. It contains a description of all the endpoints, request parameters, as well as response codes and bodies. The static files can be found on GitHub:
    * `Documentation/Swashbuckle/swagger.json <https://github.com/mihailo-stevanovic/documentation-repository/blob/master/API/Documentation/Swashbuckle/swagger.json>`_ - `Raw <https://raw.githubusercontent.com/mihailo-stevanovic/documentation-repository/master/API/Documentation/Swashbuckle/swagger.json>`_
    * `Documentation/Swashbuckle/swagger.yaml <https://github.com/mihailo-stevanovic/documentation-repository/blob/master/API/Documentation/Swashbuckle/swagger.yaml>`_ - `Raw <https://raw.githubusercontent.com/mihailo-stevanovic/documentation-repository/master/API/Documentation/Swashbuckle/swagger.yaml>`_

2. Manually edited document using Swagger Editor. It is an updated version of the Swashbuckle document with improved examples and additional information. The static files can be found on GitHub:
    * `Documentation/Swagger/swagger.json <https://github.com/mihailo-stevanovic/documentation-repository/blob/master/API/Documentation/Swagger/swagger.json>`_ - `Raw <https://raw.githubusercontent.com/mihailo-stevanovic/documentation-repository/master/API/Documentation/Swagger/swagger.json>`_
    * `Documentation/Swagger/swagger.yaml <https://github.com/mihailo-stevanovic/documentation-repository/blob/master/API/Documentation/Swagger/swagger.yaml>`_ - `Raw <https://raw.githubusercontent.com/mihailo-stevanovic/documentation-repository/master/API/Documentation/Swagger/swagger.yaml>`_

You can access the Swashbuckle document directly from the application, by navigating to ``/swagger`` after running it (e.g. ``http://localhost:56372/swagger/``).

Open in Swagger Editor
^^^^^^^^^^^^^^^^^^^^^^

To view the static files, you will need to use the **Swagger Editor**.

1. Navigate to https://editor.swagger.io/.

   A sample document displays.

2. In the main menu, click **File** > **Clear editor**.

3. In the main menu, click **File** > **Import URL**.

   A dialog pops up.

4. Enter the URL to a raw Swagger document.
   
   You can use a JSON or a YAML file.

5. Click **OK**.

   The Swagger document is now properly loaded. The right-hand part of the screen displays the Swagger UI.
   
   .. image:: graphics/SwaggerEditor_V100_Loaded.png
   
6. Explore the available endpoints.