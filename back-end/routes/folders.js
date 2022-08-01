const router = require('express').Router();

router.post('/add', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.addFolder(req);
    return res.status(response.status).json(response.data);
});

router.delete('/delete', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.deleteFolder(req);
    return res.status(response.status).json(response.data);
});

router.get('/info', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.getFolderInfo(req);
    return res.status(response.status).json(response.data);
});

router.get('/list', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.listFolders(req);
    return res.status(response.status).json(response.data);
});

module.exports = router;