const router = require('express').Router();

router.get('/list', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.list(req);
    return res.status(response.status).json(response.data);
});

router.get('/info', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.getImageInfo(req);
    return res.status(response.status).json(response.data);
});

router.post('/add', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.add(req);
    return res.status(response.status).json(response.data);
});

router.delete('/delete', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.delete(req);
    return res.status(response.status).json(response.data);
});

router.put('/favorite', async (req, res) => {
    const bl = req.app.bl.imageBL;
    const response = await bl.toggleFavorite(req);
    return res.status(response.status).json(response.data);
});

module.exports = router;